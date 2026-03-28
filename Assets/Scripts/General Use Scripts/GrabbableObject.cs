using UnityEngine;

public class GrabbableObject : InteractableObject, IInteractable
{

    public override void Interact(Player player)
    {
        if(player.IsPlayerHoldingObject()) { return; }
        // if(gameObject.TryGetComponent(out ObjectiveItem objectiveItem))
        // {
        //     objectiveItem.Interact(player);
        // }
        player.SetHeldObject(gameObject);
    }
    /*
    public override string GetInteractionText(Player player)
    {
        if (player.IsPlayerHoldingObject()) { return ""; }
        if(gameObject.TryGetComponent(out ObjectiveItem objectiveItem))
        {
            return objectiveItem.GetInteractionText(player);
        }
    }

    public override Vector3 GetInteractionPosition(Player player)
    {
        if (player.IsPlayerHoldingObject()) { return Vector3.zero; }
        if(gameObject.TryGetComponent(out ObjectiveItem objectiveItem))
        {
            return objectiveItem.GetInteractionPosition(player);
        }
        return interactPoint.position;
    }
    */

    public void HideUnselectedHotbarObject()
    {
        gameObject.SetActive(false);
    }

    public void ShowSelectedHotBarObject()
    {
        gameObject.SetActive(true);
    }
}
