using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    private float _deathTime;
    public EnemyDeathState(EnemyInstance enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Anim.Play("Death");
        _deathTime = Time.time;
        enemy.BoxCollider.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        var clipLength = enemy.Anim.GetCurrentAnimatorClipInfo(0).Length;
        if (Time.time - _deathTime > clipLength)
        {
            enemy.gameObject.SetActive(false);
        }
    }
}
