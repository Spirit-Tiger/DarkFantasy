using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private float _chaseTime = 1f;
    private float _chaseTimer;
    private int _countTrigger = 0;
    public EnemyChaseState(EnemyInstance enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Anim.Play("Walk");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.ChaseDirection();

        enemy.RB.velocity = new Vector2(enemy.MoveDirection() * -enemyData.chaseSpeed, enemy.RB.velocity.y);

        if (enemy.CanSeePlayer())
        {
            _countTrigger = 0;
        }

        if (!enemy.CanSeePlayer() && _countTrigger == 0)
        {
            _chaseTimer = Time.time;
            _countTrigger++;
        }

        if (!enemy.CanSeePlayer())
        {
            if (Time.time - _chaseTimer > _chaseTime)
            {
                stateMachine.ChangeState(enemy.IdleState);
                _countTrigger = 0;
            }
        }

        if (enemy.CanSeePlayer() && enemy.ShouldAttack())
        {
            stateMachine.ChangeState(enemy.AttackState);
        }
    }

}
