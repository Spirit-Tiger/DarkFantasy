using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingDownState : PlayerAirState
{
    public PlayerFallingDownState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.TopPartAnim.Play("LookDown");
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
