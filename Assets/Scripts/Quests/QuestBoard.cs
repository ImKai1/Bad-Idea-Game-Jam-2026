using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestBoard : MonoBehaviour, IQuest
{
    [SerializeField] private List<QuestData> quests = new List<QuestData>();
    [SerializeField] private List<QuestVisual> visuals = new List<QuestVisual>();

    [SerializeField] private GameObject VisualPrefab;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ItemData i = (ItemData)ScriptableObject.CreateInstance("ItemData"); // need an item. someone figure out the integration for this
            QuestData q = new QuestData("TEST TITLE", "Please go get me the thingy i want.", false);
            q.SetRewards(new List<QuestAdventurerRewards>() { new QuestAdventurerRewards(RewardType.money, null, 500) });
            q.SetObjectives(new List<QuestObjective>() { new QuestObjective(i, 5) });
            AddQuest(q);
        }
    }

    public void AddQuest(QuestData quest)
    {
        quests.Add(quest);

        // Instantiate visual
        QuestVisual v = Instantiate(VisualPrefab, transform).GetComponent<QuestVisual>();
        visuals.Add(v);
    }

    public QuestData AcceptQuest(QuestData q)
    {
        int idx = quests.FindIndex(element => element == q);
        quests.RemoveAt(idx);

        GameObject g = visuals.ElementAt(idx).gameObject;
        visuals.RemoveAt(idx);
        if(g != null)
            Destroy(g);

        return q;
    }

    public QuestData AcceptQuest(int idx)
    {
        return AcceptQuest(quests[idx]);
    }

    public void ReadQuest(int index)
    {
        quests.ElementAt(index);
    }
}
