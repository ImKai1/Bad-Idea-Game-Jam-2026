using UnityEngine;

public class BrewingIngredient : MonoBehaviour
{
    [SerializeField] private BrewingIngredientSO brewingIngredientSO;

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public BrewingIngredientSO GetBrewingIngredientSO()
    {
        return brewingIngredientSO;
    }
}
