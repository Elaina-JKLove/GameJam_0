using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Dash : BaseState
{
    int dashDir;

    public PlayerState_Dash(Character character, Animator animator, string animBoolName) : base(character, animator, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        int input_HorizontalMovement = (int)(character.CharacterMove as PlayerMove).Input_HorizontalMovement;
        //冲刺时优先使用输入方向而非玩家面对方向
        dashDir = input_HorizontalMovement != 0 ? input_HorizontalMovement : character.CharacterMove.FacingDir;

        stateTimer = character.CharacterMove.DashDuration;
    }

    public override void Update()
    {
        base.Update();

        character.CharacterMove.SetVelocity(character.CharacterMove.DashSpeed * dashDir, 0);

        if (stateTimer <= 0)
        {
            character.CharacterState.ChangeState(character.CharacterState.FallState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        character.CharacterMove.SetCanDash(false);
    }
}
