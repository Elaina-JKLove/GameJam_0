using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Air : BaseState
{
    public PlayerState_Air(Character character, Animator animator, string animBoolName) : base(character, animator, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        //空中也可响应方向键，但受空气阻力
        if ((character.CharacterMove as PlayerMove).Input_HorizontalMovement != 0)
            character.CharacterMove.SetVelocityX((character.CharacterMove as PlayerMove).Input_HorizontalMovement * character.CharacterMove.MoveSpeed * character.CharacterMove.AirDrag);

        if ((character.CharacterMove as PlayerMove).IsOnWall) character.CharacterState.ChangeState(character.CharacterState.WallSlidingState);
    }
}
