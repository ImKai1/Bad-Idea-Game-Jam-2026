using UnityEngine;

public class BrewingArea : MonoBehaviour
{
    [SerializeField] private BrewingCouldron brewingCouldron;

    public float explosionForce = 25f;
    public float explosionRadius = 5f;
    public float upwardForce = 10f;

    public Transform explosionPositionPoint;

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<BrewingIngredient>(out BrewingIngredient brewingIngredient)){
            brewingCouldron.TryAddIngredient(brewingIngredient);
        }
        else
        {
            Rigidbody rb;
            Debug.Log("Else");
            if(col.TryGetComponent(out rb))
            {
                Debug.Log("rb: " + rb.name);
                rb.AddExplosionForce(explosionForce, explosionPositionPoint.position, explosionRadius, upwardForce, ForceMode.Impulse);
                //Vector3 force = Vector3.up + new Vector3(Random.Range(0,3), Random.Range(1, 3), Random.Range(0, 3));
                //rb.AddForce(force * 100, ForceMode.Impulse);
                Debug.Log("Explosion");
            }
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
