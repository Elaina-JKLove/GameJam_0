using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//状态基类
public class BaseState
{
    protected Character character;
    protected Animator animator;
    protected string animBoolName;
    protected float stateTimer;



    #region Public Methods

    public BaseState(Character character, Animator animator, string animBoolName)
    {
        this.character = character;
        this.animator = animator;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() => animator.SetBool(animBoolName, true);

    public virtual void Update() => stateTimer -= Time.deltaTime;

    public virtual void Exit() => animator.SetBool(animBoolName, false);

    #endregion
}
