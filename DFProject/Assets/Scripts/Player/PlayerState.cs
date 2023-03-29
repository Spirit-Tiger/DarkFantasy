using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimFinished;
    protected bool isExitingState;
    protected float startTime;

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

    }

    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishTrigger() => isAnimFinished = true;
    public virtual void Shoot()
    {
        Debug.Log("Shoot");
        player.AnimTriggers.SwichToShootAnimation();
        player.ShootAnim.Play("ShootAnim", 0, 0.01f);
    }
}
