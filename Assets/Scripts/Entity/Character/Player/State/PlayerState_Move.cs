using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Move : PlayerState_Ground
{
    public PlayerState_Move(Character character, Animator animator, string animBoolName) : base(character, animator, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        character.CharacterMove.HandleHorizontalMove();

        if ((character.CharacterMove as PlayerMove).Input_HorizontalMovement == 0) character.CharacterState.ChangeState(character.CharacterState.IdleState);
    }
}
