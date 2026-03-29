using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages the player's objectives and tracks progress towards them
//handles item collection and updates objectives accordingly


public class PlayerObjectiveManager : MonoBehaviour
{
    public static PlayerObjectiveManager Instance { get; private set; }

    public class Objective
    {
        public string objectiveID;
        public string objectiveEventID;
        public ObjectiveItemType objectiveItemType;
        public string objectiveName;
        public int progress;
        public int amountRequired;
        public bool isComplete;

        //probably want to add a unique hashset of itemIDs that have been collected for this objective to prevent duplicate collection and progress towards the objective from the same item
        //public HashSet<string> collectedItemIDs = new HashSet<string>();
        public Objective(ObjectiveDataSO data)
        {
            objectiveID = Guid.NewGuid().ToString(); //generate a unique ID for this objective instance
            objectiveEventID = data.objectiveEventID;
            objectiveItemType = data.objectiveItemType;
            objectiveName = data.objectiveName;
            progress = 0;
            amountRequired = data.itemAmountRequired;
            isComplete = false;
        }
    }

    //Player progression data
    private Dictionary<string, Objective> objectives = new Dictionary<string, Objective>();
    public enum ObjectiveItemType
    {
        Ingredient,
        Potion,
        Misc,
    }

    public event EventHandler<OnObjectiveItemInteractEventArgs> OnObjectiveItemInteract;

    public class OnObjectiveItemInteractEventArgs
    {
        public string itemID;
        public int itemAmount;
        public ObjectiveItemType objectiveItemType;
        public string objectiveEventID;
        
        public Objective objective;
    }

    private HashSet<string> collectedItems = new HashSet<string>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CreateNewObjectiveTest();
    }

    private bool TryProgressObjective(string eventID, int amount)
    {
        //check if the eventID matches any active objectives and if the amount contributes towards completing the objective. If so, update the objective progress accordingly and check for completion.
        foreach (var objective in objectives.Values)
        {
            if (objective.objectiveEventID == eventID)
            {
                objective.progress += amount;
                Debug.Log($"Progressed objective: {objective.objectiveName}, Progress: {objective.progress}/{objective.amountRequired}");
                if (objective.progress >= objective.amountRequired)
                {
                    objective.isComplete = true;
                    CompleteObjective(objective.objectiveEventID);
                    Debug.Log($"Objective complete: {objective.objectiveName}");
                }
                return true;
            }
        }
        return false;
    }

    //when player collects an item, register it here and progress the objective if necessary
    public bool CanRegisterItem(string itemID)
    {
        if (collectedItems.Contains(itemID)) { return false; }

        collectedItems.Add(itemID);
        return true;
        //Progress Objective
    }

    public void ObjectiveItemInteract(string itemID,string itemName, int itemAmount, ObjectiveItemType itemType, string objectiveEventID)
    {
        if (!CanRegisterItem(itemID)) { return; } //if the item has already been collected, don't register it again or progress the objective
        // continue if can register item
        Debug.Log("Interact and register item on manager");
        Objective objective = GetObjective(objectiveEventID, itemType);
        if (objective == null)       {
            Debug.LogError($"No objective found for event ID: {objectiveEventID}");
            return;
        }
        if(!TryProgressObjective(objectiveEventID, itemAmount)){return;} //if the item doesn't contribute towards any objective, don't trigger the event

        OnObjectiveItemInteract?.Invoke(this, new OnObjectiveItemInteractEventArgs
        {
            itemID = itemID,
            objective = objective,
        });

        
        
    }

    [ContextMenu("Create New Objective Test")]
    public void CreateNewObjectiveTest()
    {
        //Create a new objective and add it to the objectives dictionary. This will likely involve taking in some parameters to define the objective, such as the eventID to listen for, the amount required, and the type of item that contributes towards the objective.
        Objective newObjective = new Objective(ScriptableObject.CreateInstance<ObjectiveDataSO>()); //create a new objective with default data for testing purposes. This will be replaced with proper objective creation and management later on.
        objectives.Add(newObjective.objectiveEventID, newObjective);
        Debug.Log($"Created new objective: {newObjective.objectiveName}, EventID: {newObjective.objectiveEventID}, Amount Required: {newObjective.amountRequired}, Item Type: {newObjective.objectiveItemType}");
        OnObjectiveItemInteract?.Invoke(this, new OnObjectiveItemInteractEventArgs
        {
            itemID = "TestItemID",
            objective = newObjective,
        });
    }

    public Objective GetObjective(string objectiveEventID, ObjectiveItemType itemType)
    {
        if (objectives.Count > 0 && objectives.ContainsKey(objectiveEventID))
        {
            //Check if there's an objective and it matches the event ID, if so return it
            return objectives[objectiveEventID];
        }
        else if(objectives.Count > 0)
        {
            //If there's an objective but no ID match then check for value item types
            foreach (var objective in objectives.Values)
            {
                if (objective.objectiveItemType == itemType)
                {
                    return objective;
                }
            }
            return null;
        }
        else
        {
            Debug.LogError($"Objective with ID {objectiveEventID} not found.");
            return null;
        }
    }


    public void UnregisterItem(string itemID)
    {
        if (!collectedItems.Contains(itemID)) { return; }

        collectedItems.Remove(itemID);
        //Regress Objective
    }

    public void ClearCollectedItems()
    {
        collectedItems.Clear();
        //Regress Objective
    }

    public void ResetObjectives()
    {
        ClearCollectedItems();
        //Reset all objectives to initial state
    }

    public void CompleteObjective(string objectiveID)
    {
        //Mark the objective as complete and trigger any completion events or rewards
    }

    public void FailObjective(string objectiveID)
    {
        //Mark the objective as failed and trigger any failure events or consequences
    }


}
