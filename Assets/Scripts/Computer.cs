using System;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] private KeyCode _prompt;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private float _outlineWidth = 3f;
    [SerializeField] private Canvas _screen;
    
    private Outline _outline;
    private Movement _movement = null;
    private bool _isOpen;
    
    public KeyCode Prompt => _prompt;
    
    public bool CanInteract { get; set; }

    public event Action ScreenOpened;
    public event Action ScreenClosed;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    private void Start()
    {
        _screen.gameObject.SetActive(false);
        _outline.OutlineWidth = 0;
        _isOpen = false;
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
        
        _isOpen = false;
        
        ScreenClosed?.Invoke();
    }
    
    public void Interact(Interactor interactor, Movement movement)
    {
        if (CanInteract == false || _isOpen)
            return;
        
        _screen.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;

        _cameraController.DisableCameraMovement();
        
        _movement = movement;
        _movement.CanMove = false;

        _isOpen = true;
        
        ScreenOpened?.Invoke();
    }
}
