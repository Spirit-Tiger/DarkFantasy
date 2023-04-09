using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private SpawnerData _spawnObject;
    [SerializeField]
    private int _spawnCounter;
    public int SpawnCounter { get { return _spawnCounter; } }

    private int _spawnDiedCounter;
    public int SpawnDiedCounter { get { return _spawnDiedCounter; } }

    private WaitForSeconds _spawnDelay;

    public event Action OnEnemyKilled;

    private void Spawn()
    {
        _spawnDelay = new WaitForSeconds(_spawnObject.SpawnDelay());

        if (_spawnCounter > 0)
        {
            StartCoroutine(SpawnWithDelayCoroutine(_spawnDelay));
            _spawnCounter--;
        }
    }

    IEnumerator SpawnWithDelayCoroutine(WaitForSeconds spawnDelay)
    {
        yield return spawnDelay;
        var enemyInstance = Instantiate(_spawnObject.EnemyPrefab, transform.position, Quaternion.identity);
        enemyInstance.GetComponent<Enemy>().Init(SpawnedEnemyDied);

        enemyInstance.transform.localScale = new Vector3(
            enemyInstance.transform.localScale.x * Mathf.Sign(UnityEngine.Random.Range(-1, 1)),
            enemyInstance.transform.localScale.y,
            enemyInstance.transform.localScale.z
            );

        Spawn();
    }

    private void SpawnedEnemyDied()
    {
        _spawnDiedCounter++;
        OnEnemyKilled();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Spawn();
        }
    }
}
