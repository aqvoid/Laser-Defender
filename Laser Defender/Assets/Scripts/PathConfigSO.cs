using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Path Config", fileName = "New Path Config")]
public class PathConfigSO : ScriptableObject
{
    [SerializeField] private Transform pathPrefab;
    [SerializeField] private float moveSpeed = 5f;

    public Transform GetStartingWaypoint() => pathPrefab.GetChild(0);

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();

        foreach (Transform child in pathPrefab) waypoints.Add(child);
        return waypoints;
    }

    public float GetMoveSpeed() => moveSpeed;
}
