using UnityEngine;

[System.Serializable]
public class QuestData
{
    public ItemData requestedItem;
    public int itemCount;
    public int bounty;
    public string title;
    public string desc;

    public QuestData(ItemData requestedItem, int itemCount, int bounty, string title, string desc)
    {
        this.requestedItem = requestedItem;
        this.itemCount = itemCount;
        this.bounty = bounty;
        this.title = title;
        this.desc = desc;
    }
}
