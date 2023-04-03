using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
        : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        player.RB.velocity = Vector2.zero;
        player.TopPartAnim.Play("Idle");
        player.BottomPartAnim.Play("Idle");

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }

            if (DownInput)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }

            if (LookUpInput)
            {
                stateMachine.ChangeState(player.IdleUpState);
            }
        }
    }
}
