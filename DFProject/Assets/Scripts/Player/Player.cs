using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public Animator Anim { get; private set; }

    [SerializeField]
    private Transform groundCheckPos;
    [SerializeField] 
    private LayerMask groundLayer;

    [SerializeField]
    private PlayerData playerData;

    public PlayerInputHandler PlayerInput { get; private set; }
    public Rigidbody2D RB { get; private set; }

    public int FaceingDirection { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }


    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        Anim = GetComponentInChildren<Animator>();
        PlayerInput = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "jupm");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
        FaceingDirection = 1;
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void CheckForFlip(int xInput)
    {
        if(xInput !=0 && xInput != FaceingDirection) {
            Flip();
        }
    }

    public bool CheckIfGrounded() {
        return Physics2D.OverlapCircle(groundCheckPos.position, playerData.groundCheckRadius, groundLayer);
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void Flip()
    {
        FaceingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
