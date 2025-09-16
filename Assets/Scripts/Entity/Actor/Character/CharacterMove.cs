using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public Collider2D Col => col;
    public Rigidbody2D Rb => rb;
    public int FacingDir => facingDir;
    public float MoveSpeed => moveSpeed;
    public float AirDrag => airDrag;
    public float DashSpeed => dashSpeed;
    public float DashDuration => dashDuration;
    public float JumpForce => jumpForce;
    public bool IsGrounded => isGrounded;
    public bool IsWall => isWall;



    //Cache
    protected Collider2D col;
    protected Rigidbody2D rb;

    //移动
    protected int facingDir;
    protected float moveSpeed;
    protected float airDrag;

    //冲刺
    protected float dashSpeed;
    protected float dashDuration;
    protected float dashCooldown;
    protected float dashCooldownTimer;
    protected bool canDash;

    //跳跃
    protected float jumpForce;
    protected int maxJumpCount;
    protected int canJumpCount;

    //地面探测
    protected bool isGrounded;
    [SerializeField] protected Transform groundCheckPoint;
    protected float groundCheckDistance;

    //墙面探测
    protected bool isWall;
    [SerializeField] protected Transform wallCheckPoint;
    protected float wallCheckDistance;



    protected virtual void Awake()
    {
        //Cache
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        //移动
        facingDir = 1;
        airDrag = 0.8f;

        //地面探测
        isGrounded = false;
        groundCheckDistance = 0.01f;

        //墙面探测
        isWall = false;
        wallCheckDistance = 0.01f;
    }

    protected virtual void Update()
    {
        HandleDashTimer();
        HandleGroundDetection();
        HandleWallDetection();
    }

    #region Public Methods

    public void HandleIdle() => SetVelocity(0, 0);

    public virtual void HandleHorizontalMove() { }

    public virtual void HandleAirHorizontalMove() { }

    public void HandleJump() => SetVelocityY(jumpForce);

    public bool CanJump() => canJumpCount > 0;

    public void ResetJumpCount() => canJumpCount = maxJumpCount;

    public void ReduceJumpCount(int value = 1) => canJumpCount -= value;

    public void HandleDash(int dashDir) => SetVelocity(dashSpeed * dashDir, 0);

    public bool CanDash() => canDash;

    public void SetCanDash(bool value) => canDash = value;

    public bool IsFall() => rb.velocity.y < 0;

    #endregion

    #region Protected Methods

    protected void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);

        //移动方向和面朝方向相反时水平翻转
        if (xVelocity * facingDir < 0) Flip();
    }

    protected void SetVelocityX(float xVelocity) => SetVelocity(xVelocity, rb.velocity.y);

    protected void SetVelocityY(float yVelocity) => SetVelocity(rb.velocity.x, yVelocity);

    //处理墙面探测
    protected void HandleWallDetection() => isWall = Physics2D.Raycast(wallCheckPoint.position, Vector2.right * facingDir, wallCheckDistance, GameLayer.GroundLayerMask);

    #endregion

    #region Private Methods

    //水平翻转
    void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingDir = -facingDir;
    }

    //处理冲刺冷却
    void HandleDashTimer()
    {
        if (!canDash)
        {
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0)
            {
                canDash = true;
                dashCooldownTimer = dashCooldown;
            }
        }
    }

    //处理地面探测
    void HandleGroundDetection() => isGrounded = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckDistance, GameLayer.GroundLayerMask);

    #endregion
}
