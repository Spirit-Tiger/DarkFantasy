using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerInteraction : MonoBehaviour, IDamagable
{
    [SerializeField]
    private Stats _playerStats;

    [SerializeField]
    private Transform _playerPosition;

    [SerializeField]
    private PlayerPosition _respawnPosition;

    [SerializeField]
    private Image _fadePanel;

    [SerializeField]
    private Animator _topAnimator;
    [SerializeField]
    private Animator _bottomAnimator;

    [SerializeField]
    private Player _player;

    private int _health;
    private int _damage;

    private void Start()
    {
        _health = _playerStats.Health;
        _damage = _playerStats.Damage;
        _respawnPosition.Position = _playerPosition.position;
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
        _player.StateMachine.ChangeState(_player.DeathState);

        _fadePanel.DOFade(1f, 2f).OnComplete(() =>
            {
                _playerPosition.position = _respawnPosition.Position + new Vector3(2, 5f, 2);
                _fadePanel.DOFade(0f, 1f);
                _player.StateMachine.ChangeState(_player.IdleState);
            }
            );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            Die();
        }
    }
}
