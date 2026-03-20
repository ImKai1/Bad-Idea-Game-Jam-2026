using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    private PlayerObjectiveManager objectiveManager;

    void Start()
    {
        objectiveManager = PlayerObjectiveManager.Instance;

        objectiveManager.OnObjectiveItemInteract += OnObjectiveItemInteract;
    }

    void OnObjectiveItemInteract(object sender, PlayerObjectiveManager.OnObjectiveItemInteractEventArgs eventArgs)
    {
        //get all the event data from the event args to update the UI accordingly
        string itemID = eventArgs.itemID;
        string itemName = eventArgs.itemName;
        int itemAmount = eventArgs.itemAmount;
        ObjectiveDataSO objectiveData = eventArgs.objectiveData;
        PlayerObjectiveManager.ObjectiveItemType itemType = objectiveData.objectiveItemType;
        string objectiveID = objectiveData.eventID;
        string objectiveName = objectiveData.objectiveName;
        int itemAmountRequired = objectiveData.itemAmountRequired;

        //Set the UI elements to show the item that was collected and how much it contributes towards the objective. This will likely involve updating text and images in the UI to reflect the collected item and its contribution towards the objective.
        
        //Temporarily just log the collected item for testing purposes
        Debug.Log($"Collected item: {itemName} (ID: {itemID}),\nItem Amount: {itemAmount},\nType: {itemType},\nObjectiveName: {objectiveName},\nObjectiveID: {objectiveID},\nItem Amount Required: {itemAmountRequired}");

    }
}
