using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private SpawnerData _spawnObject;
    [SerializeField]
    private int _spawnCounter;

    public IDamagable PlayerInteractions;

    private int _initSpawnCounter;
    public int SpawnCounter { get { return _spawnCounter; } }

    private int _spawnDiedCounter;
    public int SpawnDiedCounter { get { return _spawnDiedCounter; } }

    private WaitForSeconds _spawnDelay;

    public event Action OnEnemyKilled;
    public event Action OnReset;

    private List<GameObject> _enemySpawned = new List<GameObject>();

    private void Start()
    {
        _initSpawnCounter = _spawnCounter;
    }
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
        enemyInstance.GetComponent<Enemy>().Rest(OnReset);
        _enemySpawned.Add(enemyInstance);

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
        if (OnEnemyKilled != null)
        {
            OnEnemyKilled();
        }

    }

    public void Restart()
    {
        foreach (var enemy in _enemySpawned)
        {
            Destroy(enemy.gameObject);
        }
        _spawnDiedCounter = 0;
        _spawnCounter = _initSpawnCounter;
        StopAllCoroutines();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.TryGetComponent<IDamagable>(out IDamagable playerInteractions);
            PlayerInteractions = playerInteractions;
            Spawn();
        }
    }
}
