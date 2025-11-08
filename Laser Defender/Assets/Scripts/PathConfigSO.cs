using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Path Config", fileName = "New Path Config")]
public class PathConfigSO : ScriptableObject
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform pathPrefab;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int enemiesNumberToSpawn;
    [SerializeField] private float enemySpawnDelay = 1f;
    [SerializeField] private float spawnTimeVariance = 0f;
    [SerializeField] private float minSpawnTime = 0.2f;

    public int GetEnemyCount() => enemyPrefabs.Count;
    public GameObject GetEnemyPrefab(int index) => enemyPrefabs[index];


    public Transform GetStartingWaypoint() => pathPrefab.GetChild(0);

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();

        foreach (Transform child in pathPrefab) waypoints.Add(child);
        return waypoints;
    }

    public float GetMoveSpeed() => moveSpeed;

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(enemySpawnDelay - spawnTimeVariance, enemySpawnDelay + spawnTimeVariance);

        return Mathf.Clamp(spawnTime, minSpawnTime, float.MaxValue);
    }

    public GameObject GetRandomEnemy() => GetEnemyPrefab(Random.Range(0, GetEnemyCount()));
    public GameObject GetFirstEnemy() => GetEnemyPrefab(0);

    public int GetEnemiesNumber() => enemiesNumberToSpawn;
}
