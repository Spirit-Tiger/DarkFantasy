using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{

    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool grabInput;
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

        yInput = player.PlayerInput.NormInputY;
        grabInput = player.PlayerInput.GrabInput;


        if (JumpInput)
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
     
        }
    }
}
