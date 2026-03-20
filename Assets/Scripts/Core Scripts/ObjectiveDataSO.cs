using UnityEngine;

[CreateAssetMenu(menuName = "Objectives/Objective", fileName = "Objective", order = 0)]
public class ObjectiveDataSO : ScriptableObject
{
    //data for an objective, such as the name, event to listen for, and amount required

    [Header("Objective Info")]
    [Header("Comes with default values for testing purposes, but these can be changed for each objective you create.")]

    [Tooltip("The name of the objective, e.g. 'Collect 5 Ingredients'")]
    public string objectiveName = "Collect 5 Ingredients";

    [Tooltip("The event ID that this objective listens for to track progress, e.g. 'Ingredient_Collected'")]
    public string eventID = "Ingredient_Collected";

    [Tooltip("The amount required to complete the objective, e.g. 5 for 'Collect 5 Ingredients'")]
    public int itemAmountRequired = 5;

    [Tooltip("The type of item this objective requires, e.g. Ingredient, Potion, etc. This is used to determine which items contribute to this objective.")]
    public PlayerObjectiveManager.ObjectiveItemType objectiveItemType = PlayerObjectiveManager.ObjectiveItemType.Ingredient; //the type of the item that contributes to this objective, e.g. 'Ingredient' for 'Collect 5 Ingredients'
}
