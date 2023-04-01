using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerAbilityState
{
    private int xInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool jumpInput;
    public bool isCoyoteTimeActive;
    private bool grabInput;
    private bool isTouchingWallBack;
    private bool crouchInput;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckCoyoteTime();
        xInput = player.PlayerInput.NormInputX;
        jumpInput = player.PlayerInput.JumpInput;
        grabInput = player.PlayerInput.GrabInput;
        crouchInput = player.PlayerInput.CrouchInput;
        if (isGrounded && player.RB.velocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (jumpInput && (isTouchingWall || isTouchingWallBack))
        {
            isTouchingWall = player.CheckIfTouchingWall();
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (isTouchingWall && grabInput)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (isTouchingWall && xInput == player.FaceingDirection && player.RB.velocity.y <= 0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else if (Mathf.Abs(xInput) > 0f)
        {
            Move();
        }

        if (lookUpInput)
        {
            player.TopPartAnim.Play("LookUp");
        }

        if (!lookUpInput)
        {
            player.TopPartAnim.Play("Jump");
        }

        /*  if (crouchInput && isGrounded)
          {
              stateMachine.ChangeState(player.CrouchIdleState);
          }*/
    }
   
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void Move()
    {
        player.RB.velocity = new Vector2(1f * playerData.moveSpeed * xInput, player.RB.velocity.y);
        player.CheckForFlip(xInput);
    }

    private void CheckCoyoteTime()
    {
        if (isCoyoteTimeActive && Time.time > startTime + playerData.coyoteTime)
        {

            if (player.JumpState.AmountOfJumpsLeft == playerData.amountOfJumps)
            {
                Debug.Log("Coyoteeee");
                isCoyoteTimeActive = false;
                player.JumpState.DecreseAmountOfJumpsLeft();
            }
            isCoyoteTimeActive = false;
        }
    }

    public void ActivateCoyoteTime() => isCoyoteTimeActive = true;
}
