using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectiveManager : MonoBehaviour
{
    public enum ObjectiveItemType
    {
        Ingredients,
        Potions,
        Misc,
    }

    public event EventHandler OnObjectiveItemProgressed;


    public class OnObjectiveItemProgressedEventArgs
    {
        public int itemAmount;
    }

    private HashSet<string> collectedItems = new HashSet<string>();

    public void RegisterItem(string itemID)
    {
        if (collectedItems.Contains(itemID)) { return; }

        collectedItems.Add(itemID);
        //Progress Objective
    }

}
