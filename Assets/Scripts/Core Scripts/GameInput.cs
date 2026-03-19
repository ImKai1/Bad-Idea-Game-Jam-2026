using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    // Interaction Events
    public event EventHandler OnInteractAction;
    public event EventHandler OnMainAction;
    public event EventHandler OnSecondaryAction;
    public event EventHandler<int> OnHotbarSlotSelected;
    public event EventHandler<int> OnHotbarSlotCycled;

    // Movement Events
    public event EventHandler OnJumpAction;
    public event EventHandler OnCrouchStarted;
    public event EventHandler OnCrouchCanceled;
    public event EventHandler OnSprintStarted;
    public event EventHandler OnSprintCanceled;

    // Menu Events
    public event EventHandler OnExtraHUD;
    public event EventHandler OnPauseAction;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        // Interaction Actions
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.MainAction.performed += MainAction_performed;
        playerInputActions.Player.SecondaryAction.performed += SecondaryAction_performed;

        //Hotbar Actions
        playerInputActions.Player.HotbarAction.performed += HotbarAction_performed;
        
        // Movement Actions
        playerInputActions.Player.Jump.performed += Jump_performed;
        playerInputActions.Player.Crouch.started += Crouch_started;
        playerInputActions.Player.Crouch.canceled += Crouch_canceled;
        playerInputActions.Player.Sprint.started += Sprint_started;
        playerInputActions.Player.Sprint.canceled += Sprint_canceled;

        // Menu Actions
        playerInputActions.Player.ExtraHUD.performed += ExtraHUD_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void HotbarAction_performed(InputAction.CallbackContext context)
    {
        //int index = Mathf.RoundToInt(context.ReadValue<float>());
        int bindingIndex = context.action.GetBindingIndexForControl(context.control);

        if(bindingIndex < 3)
        {
            Debug.Log("Binding Index: " + bindingIndex);
            OnHotbarSlotSelected?.Invoke(this, bindingIndex);
        }
        else
        {
            float value = context.ReadValue<float>();

            int direction = value > 0 ? 1 : -1;

            OnHotbarSlotCycled?.Invoke(this, direction);
        }


    }

    private void Interact_performed(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Sprint_canceled(InputAction.CallbackContext context)
    {
        OnSprintCanceled?.Invoke(this, EventArgs.Empty);
    }

    private void Sprint_started(InputAction.CallbackContext context)
    {
        OnSprintStarted?.Invoke(this, EventArgs.Empty);
    }

    private void ExtraHUD_performed(InputAction.CallbackContext context)
    {
        OnExtraHUD?.Invoke(this, EventArgs.Empty);
    }

    private void SecondaryAction_performed(InputAction.CallbackContext context)
    {
        OnSecondaryAction?.Invoke(this, EventArgs.Empty);
    }

    private void MainAction_performed(InputAction.CallbackContext context)
    {
        OnMainAction?.Invoke(this, EventArgs.Empty);
    }

    private void Crouch_canceled(InputAction.CallbackContext context)
    {
        OnCrouchCanceled?.Invoke(this, EventArgs.Empty);
    }

    private void Crouch_started(InputAction.CallbackContext context)
    {
        OnCrouchStarted?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(InputAction.CallbackContext context)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public Vector2 GetMouseVector()
    {
        Vector2 inputVector = playerInputActions.Player.Look.ReadValue<Vector2>();
        //inputVector = inputVector.normalized;
        return inputVector;
    }

    public string GetActionKeyName(InputAction inputAction)
    {
        return inputAction.GetBindingDisplayString();
    }

    
    public void ChangeInputActionBinding(InputAction inputAction)
    {
        /*inputAction.Disable();
        inputAction.binding
        inputAction.actionMap.ChangeBinding(inputAction.GetBindingIndex()).OnComplete(operation =>
        {
            operation.Dispose();
            inputAction.Enable();
        }).Start();
        inputAction.Enable();*/
    }

    public void ChangeJumpInputActionBinding()
    {
        ChangeInputActionBinding(playerInputActions.Player.Jump);
    }

    private void OnDestroy()
    {
        playerInputActions.Dispose();
    }
}
