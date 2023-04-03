using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleUpState : PlayerGroundedState
{
    public PlayerIdleUpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.RB.velocity = Vector2.zero;
        player.TopPartAnim.Play("LookUp");
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
            if (!LookUpInput)
            {
                stateMachine.ChangeState(player.IdleState);
            }

            if (xInput != 0 && !isExitingState && LookUpInput)
            {
                stateMachine.ChangeState(player.MoveUpState);
            }

            if (DownInput && LookUpInput)
            {
                stateMachine.ChangeState(player.CrouchIdleUpState);
            }
        }
    }
}
