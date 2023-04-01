using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveUpState : PlayerGroundedState
{
    public PlayerCrouchMoveUpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.TopPartAnim.Play("CrouchLookUp");
        player.BottomPartAnim.Play("CrouchRun");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Move(playerData.crouckMoveSpeed);
        if (!lookUpInput)
        {
            stateMachine.ChangeState(player.CrouchMoveState);
        }else if(!crouchInput)
        {
            stateMachine.ChangeState(player.MoveUpState);
        }else if (xInput == 0)
        {
            stateMachine.ChangeState(player.CrouchIdleUpState);
        }
    }
}
