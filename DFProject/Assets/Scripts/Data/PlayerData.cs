using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float moveSpeed = 10f;

    [Header("Jump State")]
    public float jumpSpeed = 0.2f;
    public int amountOfJumps = 2;
    public float coyoteTime = 0.1f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.3f;
    public Vector2 wallJumpAngle = new Vector2(1, 1);

    [Header("Check Variables")]
    public float groundCheckRadius = 0.2f;
    public float wallCheckDistance = 0.5f;
}
