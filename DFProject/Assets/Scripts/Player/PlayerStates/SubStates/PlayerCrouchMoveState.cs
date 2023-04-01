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
        player.BottomPartAnim.Play("CrouchRun");
        player.TopPartAnim.Play("CrouchRun");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Move(playerData.crouckMoveSpeed);
        if (!crouchInput)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        else if (xInput == 0 && crouchInput)
        {
            stateMachine.ChangeState(player.CrouchIdleState);
        }
        else if (xInput == 0 && !crouchInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }else if (lookUpInput)
        {
            stateMachine.ChangeState(player.CrouchMoveUpState);
        }
    }
}
