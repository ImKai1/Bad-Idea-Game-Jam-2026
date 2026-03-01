using System;

using TMPro;

using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private float interactRadius = 1f;
    [SerializeField] private LayerMask interactLayerMask;

    [SerializeField] private Transform interactTextTransform;
    [SerializeField] private TextMeshProUGUI interactText;

    [SerializeField] private Transform heldObjectPoint;
    [SerializeField] private GameObject heldObject;

    private GameInput gameInput;
    private IInteractable currentInteractable;

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
        Debug.Log("Interact pressed");
        if(currentInteractable == null)
        {
            return;
        }   
        currentInteractable.Interact(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(heldObject == null)
        {
            RaycastHit hit;
            if(Physics.SphereCast(Camera.main.transform.position, interactRadius, Camera.main.transform.forward, out hit, interactDistance, interactLayerMask))
            {
                if(hit.transform.TryGetComponent(out IInteractable interactable))
                {
                    // Show interaction UI
                    Debug.Log("Looking at " + interactable);
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
                    Debug.Log("Looking at " + interactable);
                    currentInteractable = interactable;
                }
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
            Debug.Log(currentInteractable);
            interactText.text = currentInteractable.GetInteractionText();
            interactTextTransform.position = currentInteractable.GetInteractionPosition();
            interactTextTransform.rotation = Quaternion.identity;
            interactTextTransform.position = Vector3.MoveTowards(currentInteractable.GetInteractionPosition(), Camera.main.transform.position, .5f);
        }
    }

    public void SetHeldObject(GameObject interactObject)
    {
        heldObject = interactObject;
        currentInteractable = null;
        if (heldObject != null)
        {
            heldObject.transform.SetParent(heldObjectPoint);
            heldObject.GetComponent<Rigidbody>().isKinematic = true;
            heldObject.transform.localPosition = Vector3.zero;
            heldObject.transform.localRotation = Quaternion.identity;
        }
    }

    public void DropHeldObject()
    {
        if(heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.transform.SetParent(null);
            heldObject = null;
        }
    }
}
