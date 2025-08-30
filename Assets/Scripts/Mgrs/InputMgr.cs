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

        inputActions.ActionMap.HorizontalMovement.performed += context => { EventCenter.Instance.EventTrigger(EventType.Event_Input_HorizontalMovement, context.ReadValue<float>()); };
        inputActions.ActionMap.HorizontalMovement.canceled += context => { EventCenter.Instance.EventTrigger(EventType.Event_Input_HorizontalMovement, context.ReadValue<float>()); };
        inputActions.ActionMap.Jump.performed += context => { EventCenter.Instance.EventTrigger(EventType.Event_Input_Jump); };
    }

    void OnDisable() => inputActions.Disable();
}
