using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGroundedState : PlayerState
{
    private bool isGrounded;
    private bool isTouchingWall;
    private bool grabInput;


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
        InAir = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
  
        grabInput = player.PlayerInput.GrabInput;

        if (JumpInput && player.JumpState.CanJump())
        {
            if (LookUpInput)
            {
                stateMachine.ChangeState(player.JumpUpState);
                return;
            }
            else if (DownInput)
            {
                stateMachine.ChangeState(player.JumpDownState);
                return;

            }
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded && player.RB.velocity.y < 0.01f)
        {
            if (LookUpInput)
            {
                stateMachine.ChangeState(player.FallingUpState);
                return;
            }
            else if (DownInput)
            {
                stateMachine.ChangeState(player.FallingDownState);
                return;
            }
            stateMachine.ChangeState(player.FallingState);
        }
        else if (isTouchingWall && grabInput)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
    }
}
