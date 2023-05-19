using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyWalkState : EnemyState
{
    public EnemyWalkState(EnemyInstance enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
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

        enemy.TurnAround();

        enemy.RB.velocity = new Vector2(enemy.MoveDirection() * -enemyData.moveSpeed, enemy.RB.velocity.y);
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(enemy.ChaseState);
        }
    }
}
