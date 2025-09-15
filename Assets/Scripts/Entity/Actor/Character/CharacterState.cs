using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public BaseState CurrentState => stateMachine.CurrentState;
    public BaseState IdleState => idleState;
    public BaseState MoveState => moveState;
    public BaseState JumpState => jumpState;
    public BaseState FallState => fallState;
    public BaseState DashState => dashState;
    public BaseState WallSlidingState => wallSlidingState;
    public BaseState WallJumpState => wallJumpState;
    public BaseState DeathState => deathState;



    //动画机参数名
    protected const string ANIM_BOOL__IDLE = "Idle";
    protected const string ANIM_BOOL__MOVE = "Move";
    protected const string ANIM_BOOL__JUMP_AND_FALL = "JumpAndFall";
    protected const string ANIM_FLOAT__VELOCITY_Y = "VelocityY";
    protected const string ANIM_BOOL__DASH = "Dash";
    protected const string ANIM_BOOL__WALL_SLIDING = "WallSliding";
    protected const string ANIM_BOOL__DEATH = "Death";

    //Cache
    StateMachine stateMachine;
    protected Character character;
    protected Animator animator;

    //状态
    protected BaseState idleState;
    protected BaseState moveState;
    protected BaseState jumpState;
    protected BaseState fallState;
    protected BaseState dashState;
    protected BaseState wallSlidingState;
    protected BaseState wallJumpState;
    protected BaseState deathState;



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

        //除了抓墙时的下落，其他所有下落都会转为下落状态
        if (character.CharacterMove.IsFall() && CurrentState != wallSlidingState) ChangeState(fallState);
        //if(玩家死亡) ChangeState(deathState);

        animator.SetFloat(ANIM_FLOAT__VELOCITY_Y, character.CharacterMove.Rb.velocity.y);
    }

    #region Public Methods

    public void ChangeState(BaseState newState) => stateMachine.ChangeState(newState);

    #endregion
}
