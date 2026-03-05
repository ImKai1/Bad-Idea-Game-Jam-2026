using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "BrewingSO/BrewingRecipeSO", fileName = "BrewingRecipeSO", order = 1)]
public class BrewingRecipeSO : ScriptableObject
{
    public List<BrewingIngredientSO> brewingIngredients;
    public PotionObjectSO recipePotionObjectSO;
}
