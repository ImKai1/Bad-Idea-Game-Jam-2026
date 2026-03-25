using UnityEngine;

public interface IInteractable
{
    public void Interact(Player player);

    public string GetInteractionText(Player player);
    public Vector3 GetInteractionPosition(Player player);
}
