using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//处理玩家状态切换
public class PlayerState : CharacterState
{
    protected override void Awake()
    {
        base.Awake();

        idleState = new PlayerState_Idle(character, animator, ANIM_BOOL_NAME__IDLE);
        moveState = new PlayerState_Move(character, animator, ANIM_BOOL_NAME__MOVE);
        jumpState = new PlayerState_Jump(character, animator, ANIM_BOOL_NAME__JUMP);
        dashState = new PlayerState_Dash(character, animator, ANIM_BOOL_NAME__DASH);
        fallState = new PlayerState_Fall(character, animator, ANIM_BOOL_NAME__FALL);
    }

    void OnEnable()
    {
        EventCenter.Instance.AddEventListener(EventType.Event_Input_Jump, TryChangeStateToJump);
        EventCenter.Instance.AddEventListener(EventType.Event_Input_Dash, TryChangeStateToDash);
    }

    void Update()
    {
        if (character.CharacterMove.IsFall()) ChangeStateToFall();
    }

    void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(EventType.Event_Input_Jump, TryChangeStateToJump);
        EventCenter.Instance.RemoveEventListener(EventType.Event_Input_Dash, TryChangeStateToDash);
    }

    #region  Private Methods

    void TryChangeStateToJump(object obj)
    {
        if (character.CharacterMove.CanJump()
        && CurrentState != character.CharacterState.DashState
        && CurrentState != character.CharacterState.JumpState)
        {
            character.CharacterMove.ReduceJumpCount();
            ChangeState(jumpState);
        }
    }

    void TryChangeStateToDash(object obj)
    {
        if (character.CharacterMove.CanDash()) ChangeState(dashState);
    }

    void ChangeStateToFall() => ChangeState(fallState);

    #endregion
}
