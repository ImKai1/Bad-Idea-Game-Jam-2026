using UnityEngine;

[CreateAssetMenu(menuName = "BrewingSO/BrewingIngredientSO", fileName = "BrewingIngredientSO", order = 0)]
public class BrewingIngredientSO : ScriptableObject
{
    public string ingredientName;
    public float ingredientWeight;
}
