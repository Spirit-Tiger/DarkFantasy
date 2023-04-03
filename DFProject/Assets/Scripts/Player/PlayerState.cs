using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public abstract class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimFinished;
    protected bool isExitingState;
    protected float startTime;
    protected bool InAir;
    protected int xInput;
    protected bool LookUpInput;
    protected bool DownInput;
    protected bool JumpInput;
    private bool isGrounded;

    private string _animBoolName;

    private Vector3 initialPosition = new Vector3(0.33f, 1.03f, 0);
    private Vector3 crouchPosition = new Vector3(0.33f, 0.7f, 0);

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this._animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
        Debug.Log(_animBoolName);
        isAnimFinished = false;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        isExitingState = true;
        ControlTopPartPosition();

    }

    public virtual void LogicUpdate()
    {
        DoChecks();
        xInput = player.PlayerInput.NormInputX;
        LookUpInput = player.PlayerInput.LookUpInput;
        DownInput = player.PlayerInput.CrouchInput;
        JumpInput = player.PlayerInput.JumpInput;
        if (player.PlayerInput.ShootInput)
        {
            Shoot();
        }
    }
   
    public virtual void DoChecks()
    {
        isGrounded = player.CheckIfGrounded();
    }

    public virtual void AnimationFinishTrigger() => isAnimFinished = true;
    public virtual void Shoot()
    {
        player.Spawner.Spawn();
        player.AnimTriggers.SwichToShootAnimation();
        if (player.PlayerInput.LookUpInput)
        {
            player.ShootAnim.Play("ShootUp", 0, 0.01f);
        }
        else if (!player.PlayerInput.LookUpInput)
        {
            player.ShootAnim.Play("ShootAnim", 0, 0.01f);
        }
    }

    public void MoveX(float speed)
    {
        player.RB.velocity = new Vector2(1f * speed * xInput, player.RB.velocity.y);
        player.CheckForFlip(xInput);
    }

    private void ControlTopPartPosition()
    {
        isGrounded = player.CheckIfGrounded();
        if (DownInput)
        {
            if (JumpInput || (player.RB.velocity.y < 0 && !isGrounded))
            {
                player.TopPartPos.localPosition = initialPosition;
                player.ShootPartPos.localPosition = initialPosition;
            }
            else
            {
                player.TopPartPos.localPosition = crouchPosition;
                player.ShootPartPos.localPosition = crouchPosition;
            }

        }
        else
        {
            player.TopPartPos.localPosition = initialPosition;
            player.ShootPartPos.localPosition = initialPosition;
        }
    }
}
