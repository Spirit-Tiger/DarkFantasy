using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private static DoorController _instance;

    public static DoorController Instance { get { return _instance; } }

    [SerializeField]
    private DoorScript _door;

    [SerializeField]
    private List<EnemySpawner> _spawnerList;
    private int _openCondition;
    private int _enemyDiedCount;

    private void Awake()
    {
        if (_instance == null)
        { 
            _instance = this;
        }
        else if (_instance == this)
        { 
            Destroy(gameObject); 
        }
        foreach(EnemySpawner spawner in _spawnerList)
        {
            spawner.OnEnemyKilled += EnemyDied;
        }
    }
    

    private void Start()
    {
        FormGoalForOpening();
    }

    private void OnDisable()
    {
        foreach (EnemySpawner spawner in _spawnerList)
        {
            spawner.OnEnemyKilled -= EnemyDied;
        }
    }
    private void FormGoalForOpening()
    {
        foreach(EnemySpawner spawner in _spawnerList)
        {
            _openCondition += spawner.SpawnCounter;
        }
    }

    private void EnemyDied()
    {
        _enemyDiedCount++;
        if(_enemyDiedCount == _openCondition)
        {
            _door.Open();
        }
    }
}
