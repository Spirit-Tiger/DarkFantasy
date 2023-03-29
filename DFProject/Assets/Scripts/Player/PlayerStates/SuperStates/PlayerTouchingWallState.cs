using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{

    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool jumpInput;
    protected bool grabInput;
    protected int xInput;
    protected int yInput;
 
    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.PlayerInput.NormInputX;
        yInput = player.PlayerInput.NormInputY;
        grabInput = player.PlayerInput.GrabInput;
        jumpInput = player.PlayerInput.JumpInput;


        if (jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if(!isTouchingWall || (xInput != player.FaceingDirection && !grabInput))
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
