using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//处理玩家移动
public class PlayerMovement : MonoBehaviour
{
    #region Private Variables

    //Cache
    Player player;
    Rigidbody2D rb;

    //移动相关参数
    float input_HorizontalMovement;
    int facingDir;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    int maxJumpCount;
    int canJumpCount;

    //地面探测
    bool isGrounded;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] float groundCheckDistance;
    bool isOnWall;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float wallCheckDistance;

    #endregion



    void Awake()
    {
        //Cache
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2f;

        //移动相关参数
        facingDir = 1;
        moveSpeed = 5f;
        jumpForce = 7.5f;
        maxJumpCount = 1;
        canJumpCount = maxJumpCount;

        //地面探测
        isGrounded = false;
        groundCheckDistance = 0.01f;
        isOnWall = false;
        wallCheckDistance = 0.01f;
    }

    void OnEnable()
    {
        EventCenter.Instance.AddEventListener(EventType.Event_Input_HorizontalMovement, SetInput_HorizontalMovement);
        EventCenter.Instance.AddEventListener(EventType.Event_Input_Jump, HandleJump);
    }

    void Update()
    {
        HandleHorizontalMovement();
    }

    void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(EventType.Event_Input_HorizontalMovement, SetInput_HorizontalMovement);
        EventCenter.Instance.RemoveEventListener(EventType.Event_Input_Jump, HandleJump);
    }



    #region Public Methods

    #endregion

    #region Private Methods

    //设置水平运动输入
    void SetInput_HorizontalMovement(object obj)
    {
        input_HorizontalMovement = (float)obj;
    }

    //处理水平运动
    void HandleHorizontalMovement()
    {
        rb.velocity = new Vector2(moveSpeed * input_HorizontalMovement, rb.velocity.y);

        //移动方向和面朝方向相反时水平翻转
        if (input_HorizontalMovement * facingDir < 0) Flip();
    }

    //水平翻转
    void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingDir = -facingDir;
    }

    //处理跳跃
    void HandleJump(object obj)
    {
        if (canJumpCount-- > 0) rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //处理地面探测
        isGrounded = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckDistance, GameLayers.GroundLayerMask);
        //如果接触到地面则重置可跳跃次数
        if (isGrounded) canJumpCount = maxJumpCount;

        //处理墙面检测
        isOnWall = Physics2D.Raycast(wallCheckPoint.position, Vector2.right * facingDir, wallCheckDistance, GameLayers.GroundLayerMask);
        if (isOnWall) Debug.Log("接触墙面");
    }

    #endregion
}
