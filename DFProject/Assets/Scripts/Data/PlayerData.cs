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


    [Header("Check Variables")]
    public float groundCheckRadius = 0.2f;
}
