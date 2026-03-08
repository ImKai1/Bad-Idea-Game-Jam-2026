using TMPro;
using UnityEngine;

public class QuestVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Desc;

    public QuestVisual(string title, string desc)
    {
        SetData(title, desc);
    }

    public void SetData(string title, string desc)
    {
        Title.text = title;
        Desc.text = desc;
    }
}
