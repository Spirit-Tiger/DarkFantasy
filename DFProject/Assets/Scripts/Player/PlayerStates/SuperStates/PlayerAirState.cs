using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    private bool isGrounded;
    protected bool CanMove;
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();

        CanMove = true;
        InAir = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (CanMove && xInput != 0)
        {
            MoveX(playerData.moveSpeed);
        }
       

        if (LookUpInput)
        {
            player.TopPartAnim.Play("LookUp");
        }
        else if (DownInput)
        {
            player.TopPartAnim.Play("LookDown");
        }
        else
        {
            player.TopPartAnim.Play("Jump");
        }
        


        if (isGrounded && player.RB.velocity.y < 0.01f)
        {
            if (!isExitingState)
            {
                if (xInput == 0)
                {
                    if (DownInput && LookUpInput)
                    {
                        stateMachine.ChangeState(player.CrouchIdleUpState);
                        return;
                    }
                    else if (LookUpInput)
                    {
                        stateMachine.ChangeState(player.IdleUpState);
                        return;
                    }
                    else if (DownInput)
                    {
                        stateMachine.ChangeState(player.CrouchIdleState);
                        return;
                    }

                    stateMachine.ChangeState(player.IdleState);
                }
                else if (xInput != 0)
                {
                    if (DownInput && LookUpInput)
                    {
                        stateMachine.ChangeState(player.CrouchMoveUpState);
                        return;
                    }
                    else if (LookUpInput)
                    {
                        stateMachine.ChangeState(player.MoveUpState);
                        return;
                    }
                    else if (DownInput)
                    {
                        stateMachine.ChangeState(player.CrouchMoveState);
                        return;
                    }

                    stateMachine.ChangeState(player.MoveState);
                }
            }
        }

    }
}
