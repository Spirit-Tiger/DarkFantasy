using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IDamagable
{
    [SerializeField]
    private Stats _playerStats;

    [SerializeField]
    private PlayerPosition _respawnPosition;


    [SerializeField]
    private Animator _topAnimator;
    [SerializeField]
    private Animator _bottomAnimator;

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
        Debug.Log("TOOOOKDAMAGE");
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        _topAnimator.Play("Die");
        _bottomAnimator.Play("Die");
    }
}
