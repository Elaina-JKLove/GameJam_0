using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//处理玩家移动
public class PlayerMove : CharacterMove
{
    public float Input_HorizontalMovement => input_HorizontalMovement;
    public bool IsOnWall => isOnWall;



    float input_HorizontalMovement;

    //抓墙探测
    bool isOnWall;



    protected override void Awake()
    {
        base.Awake();

        rb.gravityScale = 2;

        //移动
        moveSpeed = 5f;

        //冲刺
        dashSpeed = 20f;
        dashDuration = 0.25f;
        dashCooldown = 0f;
        dashCooldownTimer = dashCooldown;
        canDash = true;

        //跳跃
        jumpForce = 7.5f;
        maxJumpCount = 1;
        canJumpCount = maxJumpCount;

        //抓墙探测
        isOnWall = false;
    }

    void OnEnable()
    {
        EventCenter.Instance.AddEventListener(EventType.Event_Input_HorizontalMovement, SetInput_HorizontalMovement);
    }

    protected override void Update()
    {
        base.Update();

        HandleOnWallDetection();
    }

    void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(EventType.Event_Input_HorizontalMovement, SetInput_HorizontalMovement);
    }

    #region  Private Methods

    //设置水平运动输入
    void SetInput_HorizontalMovement(object obj) => input_HorizontalMovement = (float)obj;

    //处理抓墙探测
    void HandleOnWallDetection() => isOnWall = isWall && input_HorizontalMovement * facingDir > 0;//接触墙（面朝墙）的同时输入（持续输入）与面向一致

    #endregion
}
