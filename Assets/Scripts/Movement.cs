using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    //[SerializeField] private bool 
    
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _rotationSpeed = 400f;

    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private Vector3 _lastDirection;

    private bool _isMoving;

    public bool IsMoving => _isMoving;
    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 forward = _cameraTransform.forward;
        Vector3 right = _cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();
        
        if (CanMove)
        {
            _moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");
            _isMoving = _moveDirection != Vector3.zero;
        }
        else
        {
            _isMoving = false;
        }

        if (_isMoving)
            _lastDirection = _moveDirection;
    }

    private void FixedUpdate()
    {
        if (CanMove)
            Move(_moveDirection);

        Quaternion newRotation = Quaternion.LookRotation(_lastDirection);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * _rotationSpeed);
    }

    private void Move(Vector3 direction)
    {
        _characterController.Move(direction * (_speed * Time.fixedDeltaTime));
    }
}
