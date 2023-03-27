using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerAbilityState
{
    private bool isGrounded;
    private int xInput;
    private bool jumpInput;
    public bool isCoyoteTimeActive;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        if (isGrounded && player.RB.velocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (Mathf.Abs(xInput) > 0f)
        {
            Move();
        }

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
