using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages the player's objectives and tracks progress towards them
//handles item collection and updates objectives accordingly


public class PlayerObjectiveManager : MonoBehaviour
{
    public static PlayerObjectiveManager Instance { get; private set; }


    void Awake()
    {
        Instance = this;
    }
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
        public string itemName;
        public ObjectiveDataSO objectiveData;
    }

    private HashSet<string> collectedItems = new HashSet<string>();

    //when player collects an item, register it here and progress the objective if necessary
    public bool CanRegisterItem(string itemID)
    {
        if (collectedItems.Contains(itemID)) { return false; }

        collectedItems.Add(itemID);
        return true;
        //Progress Objective
    }

    public void ObjectiveItemInteract(string itemID,string itemName, int itemAmount, ObjectiveDataSO objectiveData)
    {
        if (!CanRegisterItem(itemID)) { return; } //if the item has already been collected, don't register it again or progress the objective
        // continue if can register item
        OnObjectiveItemInteract?.Invoke(this, new OnObjectiveItemInteractEventArgs
        {
            itemID = itemID,
            itemName = itemName,
            itemAmount = itemAmount,
            objectiveData = objectiveData,
        });
        Debug.Log("Interact and register item on manager");
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
