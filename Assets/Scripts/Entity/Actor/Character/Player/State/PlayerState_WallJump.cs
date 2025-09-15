using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_WallJump : PlayerState_Air
{
    public PlayerState_WallJump(Character character, Animator animator, string animBoolName) : base(character, animator, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log(-1 * character.CharacterMove.FacingDir * character.CharacterMove.JumpForce + " " + character.CharacterMove.JumpForce);
        character.CharacterMove.SetVelocity(-1 * character.CharacterMove.FacingDir * character.CharacterMove.JumpForce, character.CharacterMove.JumpForce);
    }
}
