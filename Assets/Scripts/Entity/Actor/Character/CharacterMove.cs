using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public int FacingDir => facingDir;
    public float MoveSpeed => moveSpeed;
    public float DashSpeed => dashSpeed;
    public float DashDuration => dashDuration;
    public float JumpForce => jumpForce;
    public bool IsGrounded => isGrounded;



    //Cache
    protected Rigidbody2D rb;

    //移动
    protected int facingDir;
    protected float moveSpeed;

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
    protected bool isOnWall;
    [SerializeField] protected Transform wallCheckPoint;
    protected float wallCheckDistance;



    protected virtual void Awake()
    {
        //Cache
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2f;

        //地面探测
        isGrounded = false;
        groundCheckDistance = 0.01f;

        //墙面探测
        isOnWall = false;
        wallCheckDistance = 0.01f;
    }

    void Update()
    {
        HandleDashTimer();
    }

    #region Public Methods

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);

        //移动方向和面朝方向相反时水平翻转
        if (xVelocity * facingDir < 0) Flip();
    }

    public void SetVelocityX(float xVelocity) => SetVelocity(xVelocity, rb.velocity.y);

    public void SetVelocityY(float yVelocity) => SetVelocity(rb.velocity.x, yVelocity);

    public bool CanJump() => canJumpCount > 0;

    public void ReduceJumpCount(int value = 1) => canJumpCount -= value;

    public bool CanDash() => canDash;

    public void SetCanDash(bool value) => canDash = value;

    public bool IsFall() => rb.velocity.y < 0;

    #endregion

    #region Protected Methods

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        //处理地面探测
        isGrounded = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckDistance, GameLayer.GroundLayerMask);
        //如果接触到地面则重置可跳跃次数
        if (isGrounded) canJumpCount = maxJumpCount;

        //处理墙面检测
        isOnWall = Physics2D.Raycast(wallCheckPoint.position, Vector2.right * facingDir, wallCheckDistance, GameLayer.GroundLayerMask);
        if (isOnWall) Debug.Log("接触墙面");
    }

    #endregion
}
