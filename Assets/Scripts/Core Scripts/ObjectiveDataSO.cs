using UnityEngine;

[CreateAssetMenu(menuName = "Objectives/Objective", fileName = "Objective", order = 0)]
public class ObjectiveDataSO : ScriptableObject
{
    public string objectiveName;
    public string eventID;
    public int objectiveAmount;
}
