using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_WallJump : BaseState
{
    public PlayerState_WallJump(Character character, Animator animator, string animBoolName) : base(character, animator, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        (character.CharacterMove as PlayerMove).HandleWallJump();
    }
}
