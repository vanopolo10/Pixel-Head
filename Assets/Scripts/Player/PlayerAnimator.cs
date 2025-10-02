using UnityEngine;

[RequireComponent(typeof(Movement), typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Movement _movement;
    private Animator _animator;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("is moving", _movement.IsMoving);
    }
}
