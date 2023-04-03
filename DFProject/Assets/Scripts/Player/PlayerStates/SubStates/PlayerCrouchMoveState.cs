using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        player.TopPartAnim.Play("CrouchRun");
        player.BottomPartAnim.Play("CrouchRun");

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        MoveX(playerData.crouckMoveSpeed);

        if (!isExitingState)
        {
            if (!DownInput)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else if (xInput == 0 && DownInput)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else if (xInput == 0 && !DownInput)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if (LookUpInput)
            {
                stateMachine.ChangeState(player.CrouchMoveUpState);
            }
        }
    }
}
