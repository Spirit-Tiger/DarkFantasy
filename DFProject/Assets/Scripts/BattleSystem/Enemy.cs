using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    private Stats _enemyStats;

    private int _health;
    private int _damage;

    private void Start()
    {
        _health = _enemyStats.Health;
        _damage = _enemyStats.Damage;
    }

    public void TakeDamage(int damageReceived)
    {
        TakeHit(damageReceived);
    }

    private void TakeHit(int damageReceived)
    {
        _health -= damageReceived;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.TryGetComponent<IDamagable>(out IDamagable playerInteractions);
            playerInteractions.TakeDamage(_damage);
        }
    }
}
