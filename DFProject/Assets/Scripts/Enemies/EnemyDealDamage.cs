using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealDamage : MonoBehaviour
{
    [SerializeField]
    private Stats _enemyStats;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playerTakeDamage = collision.gameObject.GetComponentInChildren<IDamagable>();
        if(playerTakeDamage != null)
        {
            playerTakeDamage.TakeDamage(_enemyStats.Damage);
        }
     
    }
}
