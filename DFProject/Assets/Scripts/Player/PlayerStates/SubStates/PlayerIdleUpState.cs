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
        if (!lookUpInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (xInput != 0 && !isExitingState && lookUpInput)
        {
            stateMachine.ChangeState(player.MoveUpState);
        }

        if (crouchInput && lookUpInput)
        {
            stateMachine.ChangeState(player.CrouchIdleUpState);
        }
    }
}
