using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//处理玩家移动
public class PlayerMove : CharacterMove
{
    public float Input_HorizontalMovement => input_HorizontalMovement;


    float input_HorizontalMovement;


    protected override void Awake()
    {
        base.Awake();

        //移动相关参数
        facingDir = 1;
        moveSpeed = 5f;
        dashSpeed = 5f;
        dashDuration = 0.5f;
        dashCooldown = 0f;
        dashCooldownTimer = dashCooldown;
        canDash = true;
        jumpForce = 7.5f;
        maxJumpCount = 1;
        canJumpCount = maxJumpCount;
    }

    void OnEnable()
    {
        EventCenter.Instance.AddEventListener(EventType.Event_Input_HorizontalMovement, SetInput_HorizontalMovement);
    }

    void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(EventType.Event_Input_HorizontalMovement, SetInput_HorizontalMovement);
    }

    #region  Private Methods

    //设置水平运动输入
    void SetInput_HorizontalMovement(object obj) => input_HorizontalMovement = (float)obj;

    #endregion
}
