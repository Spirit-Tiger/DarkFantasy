using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    public Animator BottomPartAnim;
    public Animator TopPartAnim;
    public Animator ShootAnim;

    public Transform TopPartPos;
    public Transform ShootPartPos;
    public Transform ShootingPoint;

    public BulletsSpawner Spawner;

    public ShootAnimationTriggers AnimTriggers;

    [SerializeField]
    private Transform groundCheckPos;
    [SerializeField]
    private Transform wallCheck;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private PlayerData playerData;

    public PlayerInputHandler PlayerInput { get; private set; }
    public Rigidbody2D RB { get; private set; }

    public int FaceingDirection { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerIdleUpState IdleUpState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerMoveUpState MoveUpState { get; private set; }
    public PlayerJumpDownState JumpDownState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerJumpUpState JumpUpState { get; private set; }
    public PlayerFallingDownState FallingDownState { get; private set; }
    public PlayerFallingState FallingState { get; private set; }
    public PlayerFallingUpState FallingUpState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchIdleUpState CrouchIdleUpState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerCrouchMoveUpState CrouchMoveUpState { get; private set; }
    public DeathState DeathState { get; private set; }
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        PlayerInput = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        IdleUpState = new PlayerIdleUpState(this, StateMachine, playerData, "idleUp");

        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        MoveUpState = new PlayerMoveUpState(this, StateMachine, playerData, "moveUp");

        JumpDownState = new PlayerJumpDownState(this, StateMachine, playerData, "jupmDown");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "jupm");
        JumpUpState = new PlayerJumpUpState(this, StateMachine, playerData, "jupmUp");

        FallingDownState = new PlayerFallingDownState(this, StateMachine, playerData, "fallingDown");
        FallingState = new PlayerFallingState(this, StateMachine, playerData, "falling");
        FallingUpState = new PlayerFallingUpState(this, StateMachine, playerData, "fallingUp");

        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "wallJump");

        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchIdleUpState = new PlayerCrouchIdleUpState(this, StateMachine, playerData, "crouchUpIdle");

        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        CrouchMoveUpState = new PlayerCrouchMoveUpState(this, StateMachine, playerData, "crouchUpMove");

        DeathState = new DeathState(this, StateMachine, playerData, "death");
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
        FaceingDirection = 1;
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public void CheckForFlip(int xInput)
    {
        if (xInput != 0 && xInput != FaceingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, playerData.groundCheckRadius, groundLayer);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FaceingDirection, playerData.wallCheckDistance, groundLayer);
    }

    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FaceingDirection, playerData.wallCheckDistance, groundLayer);
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        RB.velocity = new Vector2(angle.x * velocity * direction, angle.y * velocity);
    }

    private void Flip()
    {
        FaceingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckPos.position, playerData.groundCheckRadius);
    }
}
