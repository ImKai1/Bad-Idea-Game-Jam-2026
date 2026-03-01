using UnityEngine;

public class GrabbableObject : MonoBehaviour, IInteractable
{

    [SerializeField] private string interactionText = "Interact";
    [SerializeField] private Transform interactPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        interactPoint.transform.rotation = Quaternion.identity;
    }

    public void Interact(Player player)
    {
        player.SetHeldObject(gameObject);
    }

    public string GetInteractionText()
    {
        return interactionText;
    }

    public Vector3 GetInteractionPosition()
    {
        return interactPoint.position;
    }
}
