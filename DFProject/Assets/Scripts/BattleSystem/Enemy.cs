using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    private Stats _enemyStats;

    [SerializeField]
    private EnemyInstance _enemy;

    private int _health;
    private int _damage;

    private event Action _onDie;
    private void Start()
    {
        _health = _enemyStats.Health;
        _damage = _enemyStats.Damage;
    }

    public void Init(Action spawnedDie)
    {
        _onDie = spawnedDie;
    }

    public void TakeDamage(int damageReceived)
    {
        TakeHit(damageReceived);
    }

    private void TakeHit(int damageReceived)
    {
        _health -= damageReceived;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        

        if (_onDie != null)
        {
            _onDie();
        }
        if (_enemy != null)
        {
            _enemy.StateMachine.ChangeState(_enemy.DeathState);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.TryGetComponent<IDamagable>(out IDamagable playerInteractions);
            playerInteractions.TakeDamage(_damage);
        }
    }
}
