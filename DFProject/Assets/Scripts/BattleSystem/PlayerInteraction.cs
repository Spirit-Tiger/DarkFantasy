using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Collections;

public class PlayerInteraction : MonoBehaviour, IDamagable
{
    [SerializeField]
    private ExtendedStats _playerStats;

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
    [SerializeField]
    private CapsuleCollider2D _playerSubstituteCollider;

    public SpriteRenderer TopSpriteRenderer;
    public SpriteRenderer BottomSpriteRenderer;
    public SpriteRenderer ShootSpriteRenderer;

    public static event Action<int> OnHPChange;
    public static event Action<float> OnHumanityRateChange;
    public static event Action OnPlayerDied;

    private int _health;
    private int _damage;
    private float _humanityRate;
/*
    private void OnEnable()
    {
        ButtonsActions.OnChangeHp += OnHPChange;
        ButtonsActions.OnChangeHumanity += OnHumanityRateChange;
    }

    private void OnDisable()
    {
        ButtonsActions.OnChangeHp -= OnHPChange;
        ButtonsActions.OnChangeHumanity -= OnHumanityRateChange;
    }*/
    private void Start()
    {
        _health = _playerStats.Health;
        _damage = _playerStats.Damage;
        _humanityRate = _playerStats.Humanity;
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
        else if (_health > 0)
        {
            _playerCollider.enabled = false;
            _playerSubstituteCollider.enabled = true;
            TopSpriteRenderer.color = new Color(128, 128, 128, 0.5f);
            BottomSpriteRenderer.color = new Color(128, 128, 128, 0.5f);
            ShootSpriteRenderer.color = new Color(128, 128, 128, 0.5f);
            StartCoroutine(HeartTime(new WaitForSeconds(3)));
        }
    }

    IEnumerator HeartTime(WaitForSeconds heartDelay)
    {
        yield return heartDelay;
        TopSpriteRenderer.color = new Color(255, 255, 255, 1);
        BottomSpriteRenderer.color = new Color(255, 255, 255, 1);
        ShootSpriteRenderer.color = new Color(255, 255, 255, 1);
        _playerCollider.enabled = true;
        _playerSubstituteCollider.enabled = false;

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
                OnPlayerDied();
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
                              OnPlayerDied();
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

        if (collision.CompareTag("PlusMaxHealth"))
        {
            collision.gameObject.SetActive(false);
            _playerStats.ChangeMaxHealth();
            _health += 1;
            OnHPChange(_health);
        }

        if (collision.CompareTag("Devil"))
        {
            collision.gameObject.SetActive(false);
            OnHumanityRateChange(-0.1f);
        }
    }
}
