using UnityEngine;

public interface IInteractable
{
    public KeyCode Prompt { get; }

    public bool CanInteract { set; }

    public void Interact(Interactor interactor, Movement movement);
}
