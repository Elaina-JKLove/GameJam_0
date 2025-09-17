using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Ground : BaseState
{
    public PlayerState_Ground(Character character, Animator animator, string animBoolName) : base(character, animator, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //接触到地面则重置可跳跃次数
        character.CharacterMove.ResetJumpCount();
        //接触到地面则重置可冲刺
        character.CharacterMove.SetCanDash(true);
    }
}
