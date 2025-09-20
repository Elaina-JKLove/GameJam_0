using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Death : BaseState
{
    public PlayerState_Death(Character character, Animator animator, string animBoolName) : base(character, animator, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("死了啦，都你害的！");
    }
}
