using UnityEngine;

[CreateAssetMenu(fileName = "newSpawnerData", menuName = "Data/Spawner Data")]
public class SpawnerData : ScriptableObject
{
    public GameObject EnemyPrefab;

    [Header("Spawn Interval")]
    public float MinValue = 0f;
    public float MaxValue = 0f;
    public float StepSize = 0f;

    public float SpawnDelay()
    {
        float randomDelay = Random.Range(MinValue, MaxValue);
        float numSteps = Mathf.Floor(randomDelay / StepSize);
        float adjustedSpawnDelay = numSteps * StepSize;
        return adjustedSpawnDelay;
    }
}
