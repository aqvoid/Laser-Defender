using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    private PathConfigSO pathConfig;
    private List<Transform> waypoints;
    private int waypointIndex = 0;

    private void Awake()
    {
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
    }

    private void Start()
    {
        pathConfig = enemySpawner.GetCurrentPath();
        waypoints = pathConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    private void FixedUpdate()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPos = waypoints[waypointIndex].position;
            float delta = pathConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, delta);

            if (transform.position == targetPos) waypointIndex++;
        }
        else Destroy(gameObject);
    }
}
