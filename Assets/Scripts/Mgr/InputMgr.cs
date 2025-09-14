using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

//管理所有输入
public class InputMgr : MonoSingleton<InputMgr>
{
    InputActions inputActions;

    void Awake() => inputActions = new InputActions();

    void OnEnable()
    {
        inputActions.Enable();

        inputActions.ActionMap.HorizontalMovement.performed += OnHorizontalMovementPerformedOrCanceled;
        inputActions.ActionMap.HorizontalMovement.canceled += OnHorizontalMovementPerformedOrCanceled;
        inputActions.ActionMap.Dash.performed += OnDashPerformed;
        inputActions.ActionMap.Jump.performed += OnJumpPerformed;
        inputActions.ActionMap.Interact.performed += OnInteractPerformed;
    }

    void OnDisable()
    {
        inputActions.Disable();

        inputActions.ActionMap.HorizontalMovement.performed -= OnHorizontalMovementPerformedOrCanceled;
        inputActions.ActionMap.HorizontalMovement.canceled -= OnHorizontalMovementPerformedOrCanceled;
        inputActions.ActionMap.Dash.performed -= OnDashPerformed;
        inputActions.ActionMap.Jump.performed -= OnJumpPerformed;
        inputActions.ActionMap.Interact.performed -= OnInteractPerformed;
    }

    #region  Public Methods

    public void Enable() => inputActions.Enable();

    public void Disable() => inputActions.Disable();

    #endregion

    #region  Private Methods

    void OnHorizontalMovementPerformedOrCanceled(InputAction.CallbackContext ctx) => EventCenter.Instance.EventTrigger(EventType.Event_Input_HorizontalMovement, ctx.ReadValue<float>());

    void OnDashPerformed(InputAction.CallbackContext ctx) => EventCenter.Instance.EventTrigger(EventType.Event_Input_Dash);

    void OnJumpPerformed(InputAction.CallbackContext ctx) => EventCenter.Instance.EventTrigger(EventType.Event_Input_Jump);

    void OnInteractPerformed(InputAction.CallbackContext ctx) => EventCenter.Instance.EventTrigger(EventType.Event_Input_Interact);

    #endregion
}
