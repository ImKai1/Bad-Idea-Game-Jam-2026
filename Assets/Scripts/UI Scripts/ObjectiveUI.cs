using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectiveTitleText;
    [SerializeField] private TextMeshProUGUI objectiveProgressText;
    [SerializeField] private Image progressBarFillImage;

    private PlayerObjectiveManager objectiveManager;

    void Start()
    {
        objectiveManager = PlayerObjectiveManager.Instance;

        objectiveManager.OnObjectiveItemInteract += OnObjectiveItemInteract;
    }

    void OnObjectiveItemInteract(object sender, PlayerObjectiveManager.OnObjectiveItemInteractEventArgs eventArgs)
    {
        //get all the event data from the event args to update the UI accordingly
        string itemID = eventArgs.itemID;//? Temporary

        PlayerObjectiveManager.Objective objective = eventArgs.objective;
        PlayerObjectiveManager.ObjectiveItemType itemType = objective.objectiveItemType;
        string objectiveID = objective.objectiveEventID;
        int objectiveProgress = objective.progress;
        string objectiveName = objective.objectiveName;
        int itemAmountRequired = objective.amountRequired;

        //Set the UI elements to show the item that was collected and how much it contributes towards the objective. This will likely involve updating text and images in the UI to reflect the collected item and its contribution towards the objective.
        
        //Temporarily just log the collected item for testing purposes
        Debug.Log($"Collected ID: {itemID},\nObjective Progress: {objectiveProgress},\nType: {itemType},\nObjectiveName: {objectiveName},\nObjectiveID: {objectiveID},\nItem Amount Required: {itemAmountRequired}");
        objectiveTitleText.text = objectiveName;
        objectiveProgressText.text = $"Progress: {objective.progress}/{itemAmountRequired}";
        progressBarFillImage.fillAmount = (float)objective.progress / itemAmountRequired;
    }
}
