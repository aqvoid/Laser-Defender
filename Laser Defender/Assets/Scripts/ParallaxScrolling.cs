using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    [SerializeField] private Vector2 moveSpeed;

    private Vector2 offset;
    private Material material;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void FixedUpdate()
    {
        offset = moveSpeed * Time.fixedDeltaTime;
        material.mainTextureOffset += offset;
    }
}
