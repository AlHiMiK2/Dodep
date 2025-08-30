using UnityEngine;

public interface IInteractable
{
    void CanInteract();
    void OnInteract(bool state);
}
