using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestBoard : MonoBehaviour, IQuest
{
    [SerializeField] private Dictionary<int, QuestData> quests = new Dictionary<int, QuestData>();
    [SerializeField] private GameObject VisualPrefab;
    [SerializeField] private List<QuestVisual> visuals = new List<QuestVisual>();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ItemData i = (ItemData)ScriptableObject.CreateInstance("ItemData");
            QuestData q = new QuestData(i, 1, 25, "TEST TITLE", "Adventurer needed! Please go get me the thingy i want.");
            AddQuest(q);
        }
    }

    public void AddQuest(QuestData quest)
    {
        if(quests.Count > 1)
            quests.Add(quests.Last().Key + 1, quest);
        else
            quests.Add(0, quest);

        // Instantiate visual
        QuestVisual v = Instantiate(VisualPrefab, transform).GetComponent<QuestVisual>();
        v.SetData(quest.title, quest.desc);
        visuals.Add(v);
    }

    public void AcceptQuest(QuestData quest)
    {
        int i = quests.FirstOrDefault(x => ReferenceEquals(x.Value, quest)).Key;
        quests.Remove(i);
        GameObject g = visuals.ElementAt(i).gameObject;
        visuals.RemoveAt(i);

        if(g != null)
            Destroy(g);
    }
}
