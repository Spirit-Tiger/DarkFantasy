using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.BottomPartAnim.Play("CrouchIdle");
        player.TopPartAnim.Play("CrouchIdle");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!crouchInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }else if (crouchInput && lookUpInput)
        {
            stateMachine.ChangeState(player.CrouchIdleUpState);
        }
        else if(crouchInput && xInput != 0)
        {
            stateMachine.ChangeState(player.CrouchMoveState);
        }
    }
}
