using System;

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
            heldObject = null;
        }
    }

    public bool IsPlayerHoldingObject()
    {
        return heldObject != null;
    }
}
