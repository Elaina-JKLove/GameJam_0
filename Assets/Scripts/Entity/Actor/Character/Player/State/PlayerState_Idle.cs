using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Idle : BaseState
{
    public PlayerState_Idle(Character character, Animator animator, string animBoolName) : base(character, animator, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        character.CharacterMove.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();

        if ((character.CharacterMove as PlayerMove).Input_HorizontalMovement != 0) character.CharacterState.ChangeState(character.CharacterState.MoveState);
    }
}
