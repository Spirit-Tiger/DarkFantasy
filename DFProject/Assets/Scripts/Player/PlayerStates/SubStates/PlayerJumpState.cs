using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerJumpState : PlayerAirState
{
    public int amountOfJumpsLeft;
    public int AmountOfJumpsLeft { get => amountOfJumpsLeft; private set { } }

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        player.TopPartAnim.Play("Jump");
        player.BottomPartAnim.Play("Jump");
        player.RB.velocity = new Vector2(player.RB.velocity.x, 0f);
        player.RB.AddForce(Vector2.up * playerData.jumpSpeed, ForceMode2D.Impulse);
        amountOfJumpsLeft--;
        player.PlayerInput.UseJupmInput();
      
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(player.RB.velocity.y < 0.01f)
        {
            if (LookUpInput)
            {
                stateMachine.ChangeState(player.FallingUpState);
                return;
            }else if(DownInput){
                stateMachine.ChangeState(player.FallingDownState);
                return;
            }
            stateMachine.ChangeState(player.FallingState);
        }

    }

    public bool CanJump()
    {
        return amountOfJumpsLeft > 0 ? true : false;
    }

    public void ResetAmountOfJumps() => amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreseAmountOfJumpsLeft() => amountOfJumpsLeft -= playerData.amountOfJumps;
}
