using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public List<QuestObjective> objectives = new List<QuestObjective>();
    public List<QuestAdventurerRewards> adventurerRewards = new List<QuestAdventurerRewards>();
    public string title = "";
    public string desc = "";
    public bool bonus;

    public QuestData(string title, string desc, bool bonus)
    {
        this.title = title;
        this.desc = desc;
        this.bonus = bonus;
    }

    public void SetObjectives(List<QuestObjective> objs) { objectives = objs; }
    public void SetRewards(List<QuestAdventurerRewards> rewards) { adventurerRewards = rewards; }
    public void SetTitle(string title) { this.title = title; }
    public void SetDesc(string desc) { this.desc = desc; }
    public void SetBonus(bool bonus) { this.bonus = bonus; }
}

[System.Serializable]
public struct QuestObjective
{
    public ItemData requestedItem;
    public int count;
    public bool completed;

    public QuestObjective(ItemData requestedItem, int count, bool completed = false)
    {
        this.requestedItem = requestedItem;
        this.count = count;
        this.completed = completed;
    }
}

public enum RewardType
{
    money,
    item
}

[System.Serializable]
public struct QuestAdventurerRewards
{
    public RewardType type;
    public ItemData item; // only use if applicable
    public int amount;

    public QuestAdventurerRewards(RewardType type, ItemData item, int amount)
    {
        this.type = type;
        this.item = item; // will be null if type isn't item
        this.amount = amount;
    }
}
