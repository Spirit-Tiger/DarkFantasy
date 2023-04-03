using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletsSpawner : MonoBehaviour
{
    [SerializeField]
    private Bullet _bulletPrefab;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private float _bulletSpeed = 20;
    private ObjectPool<Bullet> _bulletsPool;

    private void Awake()
    {
        _bulletsPool = new ObjectPool<Bullet>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet, false, 10, 40);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void OnTakeBulletFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.position = transform.position;
        bullet.transform.localScale = new Vector2( 1 * _player.FaceingDirection, 1);
        bullet.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * _bulletSpeed * _player.FaceingDirection;

    }

    private void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }


    private Bullet CreateBullet()
    {
        return Instantiate(_bulletPrefab); 
    }

    public void Spawn()
    {
            var bullet = _bulletsPool.Get();
            bullet.Init(KillAction);
    }

    private void KillAction(Bullet bullet)
    {
        _bulletsPool.Release(bullet);
    }
}
