using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private float _outlineWidth = 3f;
    [SerializeField] private KeyCode _prompt;
    
    [SerializeField] private Canvas _screen;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _createButton;
    [SerializeField] private Button _deleteButton;
    
    private Outline _outline;
    private Movement _movement = null;

    public KeyCode Prompt => _prompt;
    
    public bool CanInteract { get; set; }

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    private void Start()
    {
        _outline.OutlineWidth = 0;
    }

    private void Update()
    {
        _outline.OutlineWidth = CanInteract ? _outlineWidth : 0;
    }

    public void CloseScreen()
    {
        _screen.gameObject.SetActive(false);
        _movement.CanMove = true;
        
        _cameraController.EnableCameraMovement();
    }
    
    public void Interact(Interactor interactor, Movement movement)
    {
        _screen.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;

        _cameraController.DisableCameraMovement();
        
        _movement = movement;
        _movement.CanMove = false;
    }
}
