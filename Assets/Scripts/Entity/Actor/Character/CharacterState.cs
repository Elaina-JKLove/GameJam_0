using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public BaseState CurrentState => stateMachine.CurrentState;
    public BaseState IdleState => idleState;
    public BaseState MoveState => moveState;
    public BaseState JumpState => jumpState;
    public BaseState DashState => dashState;
    public BaseState FallState => fallState;



    //动画机参数名
    protected const string ANIM_BOOL_NAME__IDLE = "Idle";
    protected const string ANIM_BOOL_NAME__MOVE = "Move";
    protected const string ANIM_BOOL_NAME__JUMP = "Jump";
    protected const string ANIM_BOOL_NAME__DASH = "Dash";
    protected const string ANIM_BOOL_NAME__FALL = "Fall";

    //Cache
    StateMachine stateMachine;
    protected Character character;
    protected Animator animator;

    //状态
    protected BaseState idleState;
    protected BaseState moveState;
    protected BaseState jumpState;
    protected BaseState dashState;
    protected BaseState fallState;



    protected virtual void Awake()
    {
        stateMachine = new StateMachine();
        character = GetComponent<Character>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        stateMachine.Init(idleState);
    }

    void Update()
    {
        stateMachine.Update();
    }

    #region Public Methods

    public void ChangeState(BaseState newState) => stateMachine.ChangeState(newState);

    #endregion
}
