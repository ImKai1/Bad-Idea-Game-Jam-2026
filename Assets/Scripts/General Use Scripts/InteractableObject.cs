using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    //Base class for objects that the player can interact with in the world, such as grabbable objects or objective items. Provides default implementations for interaction text and position, which can be overridden by subclasses if necessary.
    [SerializeField] private string interactionText = "Interact";
    [SerializeField] private Transform interactPoint;

    //private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (interactPoint == null)
        {
            Debug.LogWarning("Missing interact point on InteractableObject: " + gameObject.name);
            interactPoint = transform; // Default to using the object's position if no interact point is set
        }
        if (interactionText == null)
        {
            Debug.LogWarning("Missing interaction text on InteractableObject: " + gameObject.name);
        }
        //player = Player.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Keep interact point from rotating with the object, so it always faces upright
        if(interactPoint == null)
        {
            Debug.LogWarning("Missing interact point on InteractableObject: " + gameObject.name);
            interactPoint = transform; // Default to using the object's position if no interact point is set
        }
        interactPoint.transform.rotation = Quaternion.identity;
    }

    public virtual void Interact(Player player)
    {
        
    }

    public virtual string GetInteractionText(Player player)
    {
        return interactionText;
    }

    public void SetInteractionText(string newText)
    {
        interactionText = newText;
    }

    public virtual Vector3 GetInteractionPosition(Player player)
    {
        return interactPoint.position;
    }
}
