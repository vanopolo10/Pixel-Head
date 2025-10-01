using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineOrbitalFollow), typeof(CinemachineRotationComposer))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private float _zoomSpeed = 2f;
    [SerializeField] private float _zoomLerpSpeed = 10f;
    [SerializeField] private float _minDistance = 3f;
    [SerializeField] private float _maxDistance = 15f;

    private InputSystem_Actions _inputSystemActions;
    private CinemachineOrbitalFollow _orbitalFollow;
    private CinemachineRotationComposer _rotationComposer;
    
    private Vector2 _scroll;

    private float _targetZoom;
    private float _currentZoom;

    private void Awake()
    {
        _inputSystemActions = new InputSystem_Actions();
        _orbitalFollow = GetComponent<CinemachineOrbitalFollow>();
        _rotationComposer = GetComponent<CinemachineRotationComposer>();
    }
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        _targetZoom = _currentZoom = _orbitalFollow.Radius;
    }

    private void OnEnable()
    {
        _inputSystemActions.Enable();
        _inputSystemActions.Player.Zoom.performed += OnMouseScroll;
    }
    
    private void OnDisable()
    {
        _inputSystemActions.Player.Zoom.performed -= OnMouseScroll;
    }

    private void Update()
    {
        if (_scroll.y != 0 && _orbitalFollow)
        {
            _targetZoom = Mathf.Clamp(_orbitalFollow.Radius - _scroll.y * _zoomSpeed, _minDistance, _maxDistance);
            _scroll = Vector2.zero;
        }

        _currentZoom = Mathf.Lerp(_currentZoom, _targetZoom, Time.deltaTime * _zoomLerpSpeed);
        _orbitalFollow.Radius = _currentZoom;
    }

    private void OnMouseScroll(InputAction.CallbackContext context)
    {
        _scroll = context.ReadValue<Vector2>();
    }

    private IEnumerator ReenableCameraNextFrame()
    {
        yield return null;
        
    }
    
    public void DisableCameraMovement()
    {
        _orbitalFollow.enabled = false;
        _rotationComposer.enabled = false;
    }
    
    public void EnableCameraMovement()
    {
        Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));
        Cursor.lockState = CursorLockMode.Locked;
        
        _orbitalFollow.enabled = true;
        _rotationComposer.enabled = true;
    }
}
