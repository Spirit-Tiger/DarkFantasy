using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EnemyInstance : MonoBehaviour
{
    public Rigidbody2D RB { get; private set; }
    public BoxCollider2D BoxCollider { get; private set; }
    public Animator Anim { get; private set; }
    public LayerMask PlayerLayer;
    public LayerMask GroundLayer;

    private float _rayDirection;
    private int _moveDirection;
    private bool _shouldAttack = false;

    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyWalkState WalkState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyDeathState DeathState { get; private set; }

    [SerializeField]
    private EnemyData enemyData;
    [SerializeField]
    private Transform player;
    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        BoxCollider = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<Player>().GetComponent<Transform>();
        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine, enemyData, "idle");
        AttackState = new EnemyAttackState(this, StateMachine, enemyData, "attack");
        WalkState = new EnemyWalkState(this, StateMachine, enemyData, "walk");
        ChaseState = new EnemyChaseState(this, StateMachine, enemyData, "chase");
        DeathState = new EnemyDeathState(this, StateMachine, enemyData, "death");

    }
    void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public bool CanSeePlayer()
    {
        bool seePlayer = false;

        RayCastDirection();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 10f * _rayDirection, PlayerLayer);
        Debug.DrawRay(transform.position, Vector2.left * 10f * _rayDirection, Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                seePlayer = true;
            }
            else
            {
                seePlayer = false;
            }
        }
        return seePlayer;
    }

    public void TurnAround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector2.left, 2f * _rayDirection, GroundLayer);
        Debug.DrawRay(transform.position, Vector2.left * 2f * _rayDirection, Color.green);
        if (hit.collider != null)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    public float RayCastDirection()
    {
        if (transform.localScale.x > 0)
        {
            _rayDirection = 1f;
        }
        else if (transform.localScale.x < 0)
        {
            _rayDirection = -1f;
        }
        return _rayDirection;
    }

    public void ChaseDirection()
    {
        if (Mathf.Abs((Mathf.Abs(transform.position.x) - Mathf.Abs(player.position.x))) > 2f)
        {
            if (transform.position.x > player.position.x && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else if (transform.position.x < player.position.x && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    public bool ShouldAttack()
    {
        if (Vector3.Distance(transform.position, player.position) <= 2.4f)
        {
            _shouldAttack = true;
        }
        else
        {
            _shouldAttack = false;
        }
        return _shouldAttack;
    }

    public int MoveDirection()
    {
        if (transform.localScale.x > 0)
        {
            _moveDirection = 1;
        }
        else if (transform.localScale.x < 0)
        {
            _moveDirection = -1;
        }
        return _moveDirection;
    }
}
