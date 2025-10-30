using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingLeft;

    private Vector2 minBounds;
    private Vector2 maxBounds;
    private Transform player;

    private void Start()
    {
        player = transform;

        Camera cam = Camera.main;

        minBounds = cam.ViewportToWorldPoint(Vector2.zero);
        maxBounds = cam.ViewportToWorldPoint(Vector2.one);
    }

    private void LateUpdate()
    {
        Vector2 clamped = player.position;

        clamped.x = Mathf.Clamp(clamped.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        clamped.y = Mathf.Clamp(clamped.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);

        player.position = clamped;
    }
}
