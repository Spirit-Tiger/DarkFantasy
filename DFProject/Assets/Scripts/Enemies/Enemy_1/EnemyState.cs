using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{

    protected EnemyInstance enemy;
    protected EnemyStateMachine stateMachine;
    protected EnemyData enemyData;

    private string _animBoolName;
    public EnemyState(EnemyInstance enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
        this._animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        Debug.Log("Enemy " + _animBoolName);
    }

    public virtual void Exit()
    {
    }

    public virtual void LogicUpdate()
    {
    }
}
