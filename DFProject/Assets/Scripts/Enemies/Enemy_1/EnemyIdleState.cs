using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private float _waitTime;
    public EnemyIdleState(EnemyInstance enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _waitTime = Time.time;
        enemy.Anim.Play("Idle");
        enemy.RB.velocity = Vector3.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time - 2f > _waitTime)
        {
            stateMachine.ChangeState(enemy.WalkState);
        }

        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(enemy.ChaseState);
        }
    }
}
