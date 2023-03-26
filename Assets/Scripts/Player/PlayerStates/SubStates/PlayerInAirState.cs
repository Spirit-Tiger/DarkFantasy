using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerAbilityState
{
    private bool isGrounded;
    private int xInput;
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

        xInput = player.PlayerInput.NormInputX;

        if (isGrounded && player.RB.velocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if(Mathf.Abs(xInput) > 0f)
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
}
