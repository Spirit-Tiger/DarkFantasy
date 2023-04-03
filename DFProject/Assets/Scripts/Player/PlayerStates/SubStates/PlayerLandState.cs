using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.RB.velocity = Vector2.zero;
        if (!isExitingState)
        {
            if (xInput != 0)
            {
                if (LookUpInput)
                {
                    stateMachine.ChangeState(player.MoveUpState);
                    return;
                }
                stateMachine.ChangeState(player.MoveState);
            }
            else if(LookUpInput)
            {
                    stateMachine.ChangeState(player.IdleUpState);
          
            }else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
