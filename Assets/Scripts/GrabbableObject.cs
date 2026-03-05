using UnityEngine;

public class GrabbableObject : MonoBehaviour, IInteractable
{

    [SerializeField] private string interactionText = "Interact";
    [SerializeField] private Transform interactPoint;

    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        interactPoint.transform.rotation = Quaternion.identity;
    }

    public void Interact(Player player)
    {
        if(player.IsPlayerHoldingObject()) { return; }
        player.SetHeldObject(gameObject);
    }

    public string GetInteractionText()
    {
        if (player.IsPlayerHoldingObject()) { return ""; }
        return interactionText;
    }

    public Vector3 GetInteractionPosition()
    {
        if (player.IsPlayerHoldingObject()) { return Vector3.zero; }
        return interactPoint.position;
    }
}
