using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour

{
    private event Action<Bullet> _killAction;
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private float _bulletSpeed;

    private WaitForSeconds _deactivateTimer = new (0.7f);

    public void Init(Action<Bullet> killAction)
    {
        _killAction = killAction;
        StartCoroutine(DeacticvateBullet());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent<IDamagable>(out IDamagable DamageObject);
        DamageObject?.TakeDamage(1);
        _killAction(this);
    }

    IEnumerator DeacticvateBullet()
    {
        yield return _deactivateTimer;
        _killAction(this);
    }
}
