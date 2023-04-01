using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected bool JumpInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool grabInput;
    protected bool crouchInput;
    protected bool lookUpInput;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
        : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumps();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.PlayerInput.NormInputX;
        JumpInput = player.PlayerInput.JumpInput;
        grabInput = player.PlayerInput.GrabInput;
        crouchInput = player.PlayerInput.CrouchInput;
        lookUpInput = player.PlayerInput.LookUpInput;

        if (JumpInput && player.JumpState.CanJump())
        {
            if (lookUpInput)
            {
                stateMachine.ChangeState(player.JumpUpState);
                return;
            }
                stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.ActivateCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if(isTouchingWall && grabInput)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void Move(float speed)
    {
        player.RB.velocity = new Vector2(1f * speed * xInput, player.RB.velocity.y);
        player.CheckForFlip(xInput);
    }
}
