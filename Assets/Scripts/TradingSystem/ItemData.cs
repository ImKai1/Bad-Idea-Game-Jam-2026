using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string Name;
    public int Value;
    public GameObject ItemWorldObject;

    public ItemData(string name, int value)
    {
        Name = name;
        Value = value;
    }
}
