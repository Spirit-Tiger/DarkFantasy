using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimFinished;
    protected bool isExitingState;
    protected float startTime;
    private bool isGrounded;

    private string _animBoolName;

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
    }

    public virtual void LogicUpdate()
    {
        DoChecks();
        if (player.PlayerInput.ShootInput)
        {
            Shoot();
        }
    }
    public virtual void PhysicsUpdate()
    {

    }
    public virtual void DoChecks()
    {
        isGrounded = player.CheckIfGrounded();
    }

    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishTrigger() => isAnimFinished = true;
    public virtual void Shoot()
    {
        Debug.Log("Shoot");
        player.AnimTriggers.SwichToShootAnimation();
        if (player.PlayerInput.LookUpInput)
        {
            if (player.PlayerInput.CrouchInput && isGrounded)
            {
                Debug.Log("ShootCrouchUp");
                player.ShootAnim.Play("ShootCrouchUp", 0, 0.01f);
            }
            else
            {
                Debug.Log("ShootUp");
                player.ShootAnim.Play("ShootUp", 0, 0.01f);
            }
        }
        else if(!player.PlayerInput.LookUpInput )
        {
            if (player.PlayerInput.CrouchInput && isGrounded)
            {
                player.ShootAnim.Play("ShootCrouch", 0, 0.01f);
            }
            else
            {
                Debug.Log("ShootNormal");
                player.ShootAnim.Play("ShootAnim", 0, 0.01f);
            }
        }
    }
}
