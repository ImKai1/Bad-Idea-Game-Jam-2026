using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;


//represents an item in the world that can be collected for an objective, such as an ingredient or potion
public class ObjectiveItem : InteractableObject, IInteractable
{
    

    [Tooltip("Select the ObjectiveData ScriptableObject for this item. This will determine which objective this item contributes to and how much it contributes.")]
    [SerializeField] private ObjectiveDataSO objectiveData;

    [Tooltip("The name of the item, e.g. 'Red Herb', 'Health Potion', etc. This is used for display purposes and can also be used to determine which items contribute to which objectives based on the objective's requirements.")]
    [SerializeField] private string itemName; //the name of the item, e.g. "Red Herb", "Health Potion", etc. This is used for display purposes and can also be used to determine which items contribute to which objectives based on the objective's requirements.

    [Tooltip("The icon for the item, used for display in the UI when showing the item as part of an objective or in the player's inventory.")]
    [SerializeField] private Sprite itemIcon; //the icon for the item, used for display in the UI when showing the item as part of an objective or in the player's inventory.

    [Tooltip("A description of the item, which can be shown in the UI when the player interacts with the item or views it in their inventory.")]
    [TextArea]
    [SerializeField] private string itemDescription; //a description of the item, which can be shown in the UI when the player interacts with the item or views it in their inventory.

    [Tooltip("The amount that this item contributes towards the objective. For example, if the objective is 'Collect 5 Ingredients' and this item is an ingredient, then this could be set to 1 to indicate that collecting this item contributes 1 towards the total of 5 required.")]
    [SerializeField] private int itemAmount = 1; //the amount that this item contributes towards the objective. For example, if the objective is "Collect 5 Ingredients" and this item is an ingredient, then this could be set to 1 to indicate that collecting this item contributes 1 towards the total of 5 required.

    //These fields are cached from the ObjectiveDataSO for easier access when registering the item with the PlayerObjectiveManager. This way we don't have to access the ScriptableObject every time we register the item, which can be more efficient. 
    private PlayerObjectiveManager.ObjectiveItemType objectiveItemType;
    private string objectiveEventID;

    //Automatically creates a unique item ID based on the instance ID of the game object. This ensures that each item has a unique identifier for tracking purposes.
    private string itemID => gameObject.GetInstanceID().ToString();

    private PlayerObjectiveManager objectiveManager;

    void Start()
    {
        objectiveManager = PlayerObjectiveManager.Instance;
        if (objectiveManager == null)
        {
            Debug.LogError("No PlayerObjectiveManager found in the scene. Please add one to manage objectives.");
        }

        objectiveItemType = objectiveData.objectiveItemType;
        objectiveEventID = objectiveData.objectiveEventID;
    }

    public override void Interact(Player player)
    {
        //when player interacts with this item, register it with the PlayerObjectiveManager
        if(objectiveManager == null && PlayerObjectiveManager.Instance != null)
        {
            objectiveManager = PlayerObjectiveManager.Instance;
        }


        if (objectiveManager != null)
        {
            if(objectiveEventID == null)
            {
                Debug.LogWarning($"ObjectiveItem {itemName} is missing objective event ID or item type. Please check the ObjectiveDataSO for this item.");
                objectiveItemType = objectiveData.objectiveItemType;
                objectiveEventID = objectiveData.objectiveEventID;
            }
            
            Debug.Log($"Registering item with PlayerObjectiveManager: {itemName}, Amount: {itemAmount}, Type: {objectiveItemType}, EventID: {objectiveEventID}");
            ////objectiveManager.CanRegisterItem(itemID);
            objectiveManager.ObjectiveItemInteract(itemID, itemName, itemAmount, objectiveItemType, objectiveEventID);
            Debug.Log("Interacted with ObjectiveItem: " + itemName);

            //TODO: trigger the event associated with this item, e.g. "Ingredient_Collected", so that the PlayerObjectiveManager can listen for it and update the objective progress accordingly. 
            //TODO: This will likely involve creating a custom event system or using Unity's built-in event system to broadcast the event when the item is collected.
        }
        else
        {
            Debug.LogError("No PlayerObjectiveManager found in the scene. Please add one to manage objectives.");
        }

        //Handle interaction
        if(gameObject.TryGetComponent(out GrabbableObject grabbable))
        {
            // if the item is grabbable, interact with component
            grabbable.Interact(player);
        }
        else
        {
            //If the item can't be grabbed, just disable it for now to simulate it being collected. This will be replaced with proper inventory management and UI later on.
            gameObject.SetActive(false); 
        }
        
    }

    public override string GetInteractionText(Player player)
    {
        return $"Collect {itemName}";
    }

    public ObjectiveDataSO GetObjectiveData()
    {
        return objectiveData;
    }

    public string GetItemID()
    {
        return itemID;
    }
}
