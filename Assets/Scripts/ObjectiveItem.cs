using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveItem : MonoBehaviour
{
    private string itemID => gameObject.GetInstanceID().ToString();

    [SerializeField] private PlayerObjectiveManager.ObjectiveItemType objectiveItemType;

    public string GetItemID()
    {
        return itemID;
    }
}
