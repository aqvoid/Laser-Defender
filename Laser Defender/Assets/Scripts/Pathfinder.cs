using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private PathConfigSO pathConfig;

    private List<Transform> waypoints;
    private int waypointIndex = 0;

    private void Start()
    {
        waypoints = pathConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    private void Update()
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
