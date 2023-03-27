using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected bool JumpInput;
    private bool isGrounded;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
        : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
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


        if (JumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.ActivateCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

 
}
