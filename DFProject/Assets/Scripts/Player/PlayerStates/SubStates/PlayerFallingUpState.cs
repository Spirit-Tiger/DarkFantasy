using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingUpState : PlayerAirState
{
    public PlayerFallingUpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.TopPartAnim.Play("LookUp");
        player.BottomPartAnim.Play("Jump");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
