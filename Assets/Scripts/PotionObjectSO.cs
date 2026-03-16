using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PotionSO/PotionObjectSO", fileName = "PotionObjectSO", order = 0)]
public class PotionObjectSO : ScriptableObject
{
    public string potionName;
    public GameObject potionPrefab;
}
