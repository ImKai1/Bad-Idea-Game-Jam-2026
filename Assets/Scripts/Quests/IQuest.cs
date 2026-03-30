using UnityEngine;

public interface IQuest
{
    public void AddQuest(QuestData quest);
    public QuestData AcceptQuest(QuestData quest);
    public QuestData AcceptQuest(int index);
}
