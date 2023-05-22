using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationEvents : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D _spearCollider;

    public void EnableSpearCollider()
    {
        _spearCollider.enabled = true;
    }

    public void DisableSpearCollider()
    {
        _spearCollider.enabled = false;
    }

}
