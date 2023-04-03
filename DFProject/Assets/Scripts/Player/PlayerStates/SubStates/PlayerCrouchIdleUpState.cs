using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleUpState : PlayerGroundedState
{
    public PlayerCrouchIdleUpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.RB.velocity = new Vector2(0f, player.RB.velocity.y);
        player.TopPartAnim.Play("CrouchLookUp");
        player.BottomPartAnim.Play("CrouchIdle");

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
                stateMachine.ChangeState(player.CrouchMoveUpState);
            }
            if (!LookUpInput)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else if (LookUpInput && !DownInput)
            {
                stateMachine.ChangeState(player.IdleUpState);
            }
        }
    }
}
