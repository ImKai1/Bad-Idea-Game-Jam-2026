using UnityEngine;

public interface IInteractable
{
    public void Interact(Player player);

    public string GetInteractionText();

    public Vector3 GetInteractionPosition();
}
