using System;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Movement))]
public class Interactor : MonoBehaviour
{
    [SerializeField] private Collider _interactorCollider;

    private Movement _movement;
    private List<IInteractable> _interactables = new();

    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            _interactables.Add(interactable);
            
            if(_interactables.Count == 1)
                interactable.CanInteract = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            _interactables.Remove(interactable);
            interactable.CanInteract = false;

            if (_interactables.Count == 1)
                _interactables[0].CanInteract = true;
        }
    }

    private void Update()
    {
        if (_interactables.Count > 0 && Input.GetKeyDown(_interactables[0].Prompt))
            _interactables[0].Interact(this, _movement);
    }
}
