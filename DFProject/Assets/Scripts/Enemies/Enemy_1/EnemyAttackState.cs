using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float _attackTime;
    public EnemyAttackState(EnemyInstance enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _attackTime = Time.time;
        enemy.RB.velocity = Vector2.zero;
        enemy.Anim.Play("Attack");
     
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        var clipLength = enemy.Anim.GetCurrentAnimatorClipInfo(0).Length;
        if (Time.time - _attackTime > clipLength)
        {
            stateMachine.ChangeState(enemy.ChaseState);
        }
    }
}
