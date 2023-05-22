using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

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
    [SerializeField]
    private CapsuleCollider2D _playerCollider;

    public static event Action<int> OnHPChange;

    private int _health;
    private int _damage;

    private void Start()
    {
        _health = _playerStats.Health;
        _damage = _playerStats.Damage;
        _respawnPosition.Position = _playerPosition.position;
        OnHPChange(_health);
    }

    public void TakeDamage(int damage)
    {
        TakeHit(damage);
    }

    private void TakeHit(int damage)
    {

        _health -= damage;
        OnHPChange(_health);
        if (_health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        _playerCollider.enabled = false;
        _player.RB.bodyType = RigidbodyType2D.Static;
        _player.StateMachine.ChangeState(_player.DeathState);
        _fadePanel.DOFade(1f, 2f).OnComplete(() =>
            {
                _playerPosition.position = _respawnPosition.Position + new Vector3(2, 5f, 2);
                _fadePanel.DOFade(0f, 1f);
                _health = 1;
                OnHPChange(_health);
                _player.RB.bodyType = RigidbodyType2D.Dynamic;
                _playerCollider.enabled = true;
                _player.StateMachine.ChangeState(_player.IdleState);
            }
            );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            _health -= 1;
            OnHPChange(_health);
            if (_health <= 0)
            {
                Die();
            }
            else
            {
                _fadePanel.DOFade(1f, 2f).OnComplete(() =>
                          {
                              _playerPosition.position = _respawnPosition.Position + new Vector3(2, 5f, 2);
                              _fadePanel.DOFade(0f, 1f);
                              _player.StateMachine.ChangeState(_player.IdleState);
                          }
                         );
            }

        }
        if (collision.CompareTag("Health"))
        {
            collision.gameObject.SetActive(false);
            _health += 1;
            OnHPChange(_health);
        }
    }
}
