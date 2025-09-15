using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_WallSliding : BaseState
{
    public PlayerState_WallSliding(Character character, Animator animator, string animBoolName) : base(character, animator, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        //进入抓墙状态重置可跳跃次数
        character.CharacterMove.ResetJumpCount();
    }

    public override void Update()
    {
        base.Update();

        //抓墙状态下接触到地面时解除抓墙状态，转为地面闲置状态
        if (character.CharacterMove.IsGrounded) character.CharacterState.ChangeState(character.CharacterState.IdleState);
    }
}
