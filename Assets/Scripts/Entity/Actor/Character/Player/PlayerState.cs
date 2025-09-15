using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//处理玩家状态切换
public class PlayerState : CharacterState
{
    protected override void Awake()
    {
        base.Awake();

        idleState = new PlayerState_Idle(character, animator, ANIM_BOOL__IDLE);
        moveState = new PlayerState_Move(character, animator, ANIM_BOOL__MOVE);
        jumpState = new PlayerState_Jump(character, animator, ANIM_BOOL__JUMP_AND_FALL);
        fallState = new PlayerState_Fall(character, animator, ANIM_BOOL__JUMP_AND_FALL);
        dashState = new PlayerState_Dash(character, animator, ANIM_BOOL__DASH);
        wallSlidingState = new PlayerState_WallSliding(character, animator, ANIM_BOOL__WALL_SLIDING);
        wallJumpState = new PlayerState_WallJump(character, animator, ANIM_BOOL__JUMP_AND_FALL);
        deathState = new PlayerState_Death(character, animator, ANIM_BOOL__DEATH);
    }

    void OnEnable()
    {
        EventCenter.Instance.AddEventListener(EventType.Event_Input_Jump, TryChangeStateToJump);
        EventCenter.Instance.AddEventListener(EventType.Event_Input_Dash, TryChangeStateToDash);
    }

    void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(EventType.Event_Input_Jump, TryChangeStateToJump);
        EventCenter.Instance.RemoveEventListener(EventType.Event_Input_Dash, TryChangeStateToDash);
    }

    #region  Private Methods

    void TryChangeStateToJump(object obj)
    {
        if (character.CharacterMove.CanJump() && CurrentState != character.CharacterState.DashState)
        {
            character.CharacterMove.ReduceJumpCount();
            if (CurrentState == character.CharacterState.WallSlidingState) ChangeState(wallJumpState);
            else ChangeState(jumpState);
        }
    }

    void TryChangeStateToDash(object obj)
    {
        if (character.CharacterMove.CanDash()) ChangeState(dashState);
    }

    #endregion
}
