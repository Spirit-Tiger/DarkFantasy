using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveUpState : PlayerGroundedState
{
    public PlayerMoveUpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.TopPartAnim.Play("LookUp");
        player.BottomPartAnim.Play("Run");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Move(playerData.moveSpeed);
        if (!lookUpInput)
        {
            stateMachine.ChangeState(player.MoveState);
        }

        if(xInput == 0 && lookUpInput)
        {
            stateMachine.ChangeState(player.IdleUpState);
        }else if (crouchInput)
        {
            stateMachine.ChangeState(player.CrouchMoveUpState);
        }
    }
}
