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

        if (xInput != 0)
        {
            stateMachine.ChangeState(player.CrouchMoveUpState);
        }
        if (!lookUpInput)
        {
            stateMachine.ChangeState(player.CrouchIdleState);
        }
        else if (lookUpInput && !crouchInput)
        {
            stateMachine.ChangeState(player.IdleUpState);
        }

    }
}
