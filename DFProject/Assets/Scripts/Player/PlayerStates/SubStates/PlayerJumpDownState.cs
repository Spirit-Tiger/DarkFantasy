using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpDownState : PlayerAirState
{
    public PlayerJumpDownState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.TopPartAnim.Play("LookDown");
        player.BottomPartAnim.Play("Jump");
        player.RB.velocity = new Vector2(player.RB.velocity.x, 0f);
        player.RB.AddForce(Vector2.up * playerData.jumpSpeed, ForceMode2D.Impulse);
        player.JumpState.amountOfJumpsLeft--;
        player.PlayerInput.UseJupmInput();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.RB.velocity.y < 0.01f)
        {
            if (LookUpInput)
            {
                stateMachine.ChangeState(player.FallingUpState);
                return;
            }
            else if (!DownInput)
            {
                stateMachine.ChangeState(player.FallingState);
                return;
            }
            stateMachine.ChangeState(player.FallingDownState);
        }
    }
}
