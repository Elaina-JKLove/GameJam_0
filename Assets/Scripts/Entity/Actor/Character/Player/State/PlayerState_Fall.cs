using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Fall : BaseState
{
    public PlayerState_Fall(Character character, Animator animator, string animBoolName) : base(character, animator, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (character.CharacterMove.IsGrounded) character.CharacterState.ChangeState(character.CharacterState.IdleState);
        // if (WallDetected) 如果下落贴墙直接进入抓墙状态
    }
}
