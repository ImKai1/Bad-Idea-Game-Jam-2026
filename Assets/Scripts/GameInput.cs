using System;

using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnJumpAction;
    public event EventHandler OnCrouchStarted;
    public event EventHandler OnCrouchCanceled;
    public event EventHandler OnMainAction;
    public event EventHandler OnSecondaryAction;
    public event EventHandler OnExtraHUD;

    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
        playerInputActions.Player.Jump.performed += Jump_performed;
        playerInputActions.Player.Crouch.started += Crouch_started;
        playerInputActions.Player.Crouch.canceled += Crouch_canceled;
        playerInputActions.Player.MainAction.performed += MainAction_performed;
        playerInputActions.Player.SecondaryAction.performed += SecondaryAction_performed;
        playerInputActions.Player.ExtraHUD.performed += ExtraHUD_performed;
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

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public Vector2 GetMouseVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Look.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
