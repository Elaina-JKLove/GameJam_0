using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Jump : BaseState
{
    public PlayerState_Jump(Character character, Animator animator, string animBoolName) : base(character, animator, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        character.CharacterMove.SetVelocityY(character.CharacterMove.JumpForce);
    }
}
