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

        if (_player.PlayerInput.LookUpInput)
        {
            bullet.transform.localRotation = Quaternion.Euler(0, 0, 90);
            bullet.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * _bulletSpeed;
        }
        else if (_player.PlayerInput.CrouchInput && !_player.CheckIfGrounded())
        {
            bullet.transform.localRotation = Quaternion.Euler(0, 0, -90);
            bullet.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * _bulletSpeed;
        }
        else
        {
            if (_player.FaceingDirection > 0)
            {
                bullet.transform.localRotation = Quaternion.Euler(0, 0, 0);
                bullet.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * _bulletSpeed;
            }
            else
            {
                bullet.transform.localRotation = Quaternion.Euler(0, -180, 0);
                bullet.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * _bulletSpeed;
            }
        }
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
