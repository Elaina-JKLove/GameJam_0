using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Fall : PlayerState_Air
{
    public PlayerState_Fall(Character character, Animator animator, string animBoolName) : base(character, animator, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (character.CharacterMove.IsGrounded) character.CharacterState.ChangeState(character.CharacterState.IdleState);
        if ((character.CharacterMove as PlayerMove).IsOnWall) character.CharacterState.ChangeState(character.CharacterState.WallSlidingState);
    }
}
