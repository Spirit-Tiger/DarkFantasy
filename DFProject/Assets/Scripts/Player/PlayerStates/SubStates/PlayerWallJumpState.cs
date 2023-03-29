using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.PlayerInput.UseJupmInput();
        player.JumpState.ResetAmountOfJumps();
        player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        player.CheckForFlip(wallJumpDirection);
        player.JumpState.DecreseAmountOfJumpsLeft();
        Debug.Log("DIR" + wallJumpDirection);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time > startTime + playerData.wallJumpTime ) 
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -player.FaceingDirection;
        }
        else
        {
            wallJumpDirection = player.FaceingDirection;
        }
    }
}
