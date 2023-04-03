using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
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

        player.TopPartAnim.Play("Run");
        player.BottomPartAnim.Play("Run");

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        MoveX(playerData.moveSpeed);

        if (!isExitingState)
        {
            if (xInput == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if (xInput != 0 && DownInput)
            {
                stateMachine.ChangeState(player.CrouchMoveState);
            }

            if (LookUpInput)
            {
                stateMachine.ChangeState(player.MoveUpState);
            }
        }
    }
}
