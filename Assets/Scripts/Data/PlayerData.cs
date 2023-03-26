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


    [Header("Check Variables")]
    public float groundCheckRadius = 0.2f;
}
