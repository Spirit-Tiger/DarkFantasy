using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCollider : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider2D playerCollider;
    [SerializeField]
    private Transform groundCheckPosition;

    private Vector2 initialColliderSize;
    private Vector2 initialColliderOffset;

    private Vector2 PlayerColliderSize = new Vector2(0.5f,2.398f);
    private Vector2 PlayerColliderOffset = new Vector2(-0.02f, 0.69f);
    private void Start()
    {
        initialColliderSize = playerCollider.size;
        initialColliderOffset = playerCollider.offset;
    }
    public void CrouchCollider()
    {
        playerCollider.size = PlayerColliderSize;
        playerCollider.offset = PlayerColliderOffset;
     }
    public void StandCollider()
    {
        playerCollider.size = initialColliderSize;
        playerCollider.offset = initialColliderOffset;
    }

}
