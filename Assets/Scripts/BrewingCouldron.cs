

using System;
using System.Collections.Generic;

using UnityEngine;

public class BrewingCouldron : MonoBehaviour, IInteractable
{

    [SerializeField] private float brewingAreaSize;
    [SerializeField] private Vector3 brewingAreaOffset;
    [SerializeField] private LayerMask brewingIngredientLayer;

    [SerializeField] private string interactionText;
    [SerializeField] private Transform interactionPoint;

    [SerializeField] private EveryBrewingRecipeListSO everyBrewingRecipeListSO;
    
    private List<BrewingIngredientSO> currentBrewingRecipe;
    private List<BrewingIngredient> currentBrewingIngredients;

    private bool hasCompletedPotion;
    private PotionObjectSO currentPotionObject;
    

    private void Start()
    {
        currentBrewingRecipe = new List<BrewingIngredientSO>();
        currentBrewingIngredients = new List<BrewingIngredient>();
    }

    private void Update()
    {
        
    }

    public void TryAddIngredient(BrewingIngredient brewingIngredient)
    {
        if (!currentBrewingIngredients.Contains(brewingIngredient))
        {
            currentBrewingIngredients.Add(brewingIngredient);
            currentBrewingRecipe.Add(brewingIngredient.GetBrewingIngredientSO());
            TryBrewPotion();
        }
        
    }

    public void RemoveIngredient(BrewingIngredient brewingIngredient)
    {
        currentBrewingRecipe.Remove(brewingIngredient.GetBrewingIngredientSO());
        currentBrewingIngredients.Remove(brewingIngredient);
    }

    private void DeleteIngredient(BrewingIngredient brewingIngredient)
    {
        currentBrewingRecipe.Remove(brewingIngredient.GetBrewingIngredientSO());
        brewingIngredient.DestroySelf();
    }

    private void TryBrewPotion()
    {
        foreach (BrewingRecipeSO brewingRecipeSO in everyBrewingRecipeListSO.brewingRecipes)
        {
            Debug.Log("Checking recipe: " + brewingRecipeSO.name);
            Debug.Log("Current brewing recipe: " + string.Join(", ", currentBrewingRecipe.ConvertAll(i => i.ingredientName)));
            
            //Debug.Log("Recipe matches: " + CheckRecipeMatch(brewingRecipeSO.brewingIngredients, currentBrewingRecipe));
            if (CheckRecipeMatch(brewingRecipeSO.brewingIngredients, currentBrewingRecipe))
            {
                Debug.Log("You have brewed a " + brewingRecipeSO.name);
                currentPotionObject = brewingRecipeSO.recipePotionObjectSO;
                currentBrewingIngredients.ForEach(DeleteIngredient);
                currentBrewingIngredients.Clear();
                OnBrewingCompleted();
            }
        }
    }

    private void OnBrewingCompleted()
    {
        // Should allow the player to fill a potion with the liquid in the couldron
        hasCompletedPotion = true;
        interactionText = "Bottle " + currentPotionObject.potionName;
    }

    private void OnPlayerInteract(Player player)
    {
        if (hasCompletedPotion)
        {
            GameObject spawnedPotion = Instantiate(currentPotionObject.potionPrefab);
            player.SetHeldObject(spawnedPotion);
        }
    }

    private bool CheckRecipeMatch(List<BrewingIngredientSO> brewingRecipeA, List<BrewingIngredientSO> brewingRecipeB)
    {
        if(brewingRecipeA.Count == brewingRecipeB.Count)
        {
            // Both recipes have the same amount of ingredients
            // Now Check if the ingredients are the same
            foreach (BrewingIngredientSO brewingIngredientSO in brewingRecipeA)
            {
                if (!brewingRecipeB.Contains(brewingIngredientSO))
                {
                    // Recipe B does not contain ingredient from recipeA so fail
                    return false;
                }
            }
            return true;
        }
        else
        {
            // Recipes can't match if different lengths
            return false;
        }
    }

    public void Interact(Player player)
    {
        OnPlayerInteract(player);
    }

    public string GetInteractionText()
    {
        return interactionText;
    }

    public Vector3 GetInteractionPosition()
    {
        return interactionPoint.position;
    }
}
