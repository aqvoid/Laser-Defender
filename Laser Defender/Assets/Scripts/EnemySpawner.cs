using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<PathConfigSO> pathConfigs;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private int enemiesNumberToSpawn;
    [SerializeField] private bool randomizeWaves;
    [SerializeField, Tooltip("if false, then every enemy is first indexed in prefab list from current path")] 
    private bool randomizeEnemies;
    [SerializeField] private bool loopWaves;

    private PathConfigSO currentPath;

    private void Start()
    {
        StartCoroutine("SpawnWaves");
    }

    private IEnumerator SpawnWaves()
    {
        do
        {
            foreach (PathConfigSO waves in pathConfigs)
            {
                if (!randomizeWaves)
                    currentPath = waves;
                else
                    currentPath = pathConfigs[Random.Range(0, pathConfigs.Count)];

                for (int i = 0; i < enemiesNumberToSpawn; i++)
                {
                    if (!randomizeEnemies) SpawnFirstEnemy();
                    else SpawnRandomEnemy();

                    yield return new WaitForSeconds(currentPath.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while (loopWaves);

    }

    private void SpawnRandomEnemy() => Instantiate(currentPath.GetRandomEnemy(), currentPath.GetStartingWaypoint().position, Quaternion.identity, transform);

    private void SpawnFirstEnemy() => Instantiate(currentPath.GetFirstEnemy(), currentPath.GetStartingWaypoint().position, Quaternion.identity, transform);

    public PathConfigSO GetCurrentPath() => currentPath;
}
