using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IDamagable
{
    [SerializeField]
    private Stats _playerStats;

    private int _health;
    private int _damage;

    private void Start()
    {
        _health = _playerStats.Health;
        _damage = _playerStats.Damage;
    }

    public void TakeDamage(int damage)
    {
        TakeHit(damage);
    }

    private void TakeHit(int damage)
    {
        _health -= damage;
    }
}
