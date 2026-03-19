using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "BrewingSO/EveryBrewingRecipeListSO", fileName = "EveryBrewingRecipeListSO", order = 2)]
public class EveryBrewingRecipeListSO : ScriptableObject
{
    public List<BrewingRecipeSO> brewingRecipes;
}
