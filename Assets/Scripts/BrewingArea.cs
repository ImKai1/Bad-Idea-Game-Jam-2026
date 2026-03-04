using UnityEngine;

public class BrewingArea : MonoBehaviour
{
    [SerializeField] private BrewingCouldron brewingCouldron;

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<BrewingIngredient>(out BrewingIngredient brewingIngredient)){
            brewingCouldron.TryAddIngredient(brewingIngredient);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.TryGetComponent<BrewingIngredient>(out BrewingIngredient brewingIngredient))
        {
            brewingCouldron.RemoveIngredient(brewingIngredient);
        }
    }
}
