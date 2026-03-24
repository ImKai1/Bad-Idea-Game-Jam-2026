using System;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance { get; private set; }

    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private float interactRadius = 1f;
    [SerializeField] private LayerMask interactLayerMask;

    [SerializeField] private Transform interactTextTransform;
    [SerializeField] private TextMeshProUGUI interactText;

    [SerializeField] private Transform heldObjectPoint;
    private GameObject heldObject;

    private GameObject[] hotbarObjects = new GameObject[3];
    private int currentHotbarIndex;
    
    

    private GameInput gameInput;
    private IInteractable currentInteractable;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameInput = GameInput.Instance;

        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnMainAction += GameInput_OnMainAction;
        gameInput.OnHotbarSlotSelected += GameInput_OnHotBarSlotSelected;
        gameInput.OnHotbarSlotCycled += GameInput_OnHotbarSlotCycled;
    }

    private void GameInput_OnHotbarSlotCycled(object sender, int increment)
    {
        CycleHotbarIndex(increment);
    }

    private void GameInput_OnHotBarSlotSelected(object sender, int index)
    {
        SwitchHotbarSlot(index);
    }

    private void GameInput_OnMainAction(object sender, EventArgs e)
    {
        DropHeldObject();
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(currentInteractable == null)
        {
            return;
        }   
        currentInteractable.Interact(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        if(Physics.SphereCast(Camera.main.transform.position, interactRadius, Camera.main.transform.forward, out hit, interactDistance, interactLayerMask))
        {
            if(hit.transform.TryGetComponent(out IInteractable interactable))
            {
                // Show interaction UI
                currentInteractable = interactable;
            }
            else
            {
                currentInteractable = null;
            }
        }
        else
        {
            currentInteractable = null;
        }
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactDistance, interactLayerMask))
        {
            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                // Show interaction UI
                currentInteractable = interactable;
            }
        }
        

        if (currentInteractable == null)
        {
            // Hide interaction UI
            interactText.text = "";
            interactTextTransform.localPosition = Vector3.zero;
        }
        else
        {
            interactText.text = currentInteractable.GetInteractionText();
            interactTextTransform.position = currentInteractable.GetInteractionPosition();
            interactTextTransform.rotation = Quaternion.identity;
            interactTextTransform.position = Vector3.MoveTowards(currentInteractable.GetInteractionPosition(), Camera.main.transform.position, .5f);
        }
    }

    public void SetHeldObject(GameObject interactObject)
    {
        interactObject.GetComponent<Collider>().enabled = false;
        
        heldObject = interactObject;
        currentInteractable = null;
        if (heldObject != null)
        {
            heldObject.transform.SetParent(heldObjectPoint);
            if (heldObject.GetComponent<Rigidbody>() == null)
            {
                heldObject.AddComponent<Rigidbody>();
            }
            heldObject.GetComponent<Rigidbody>().isKinematic = true;
            heldObject.transform.localPosition = Vector3.zero;
            heldObject.transform.localRotation = Quaternion.identity;
            hotbarObjects[currentHotbarIndex] = heldObject;
            Debug.Log("Set object to slot " + currentHotbarIndex);
        }
    }

    public void DropHeldObject()
    {

        if(heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            heldObject.GetComponent<Collider>().enabled = true;
            heldObject.transform.SetParent(null);
            hotbarObjects[currentHotbarIndex] = null;
            Debug.Log("Removed object from slot " + currentHotbarIndex);
            heldObject = null;
        }
    }

    private void SwitchHotbarSlot(int index)
    {
        Debug.Log("Switch to slot " + index);
        // if holding something then hide object before switching hotbar slot
        if(heldObject != null)
        {
            heldObject.GetComponent<GrabbableObject>().HideUnselectedHotbarObject();
            Debug.Log("Hid object in slot " + currentHotbarIndex);
            heldObject = null;
        }

        currentHotbarIndex = index;

        //if selected slot already has an object then bring out object
        if (hotbarObjects[index] != null)
        {
            SetHeldObject(hotbarObjects[index]);
            heldObject.GetComponent<GrabbableObject>().ShowSelectedHotBarObject();
            Debug.Log("Showed object in slot " + currentHotbarIndex);
        }
        
    }

    private void CycleHotbarIndex(int increment)
    {
        Debug.Log("Cycle by " + increment);
        if (currentHotbarIndex + increment >= 0)
        {
            currentHotbarIndex = (currentHotbarIndex + increment) % hotbarObjects.Length;
        }
        else
        {
            currentHotbarIndex = currentHotbarIndex + increment + hotbarObjects.Length;
        }
            SwitchHotbarSlot(currentHotbarIndex);
    }

    public bool IsPlayerHoldingObject()
    {
        return heldObject != null;
    }
}
