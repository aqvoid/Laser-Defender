using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeMagnitude;

    private Health playerHealth;

    private Vector3 initialPosition;

    private void Awake() => playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnEnable() => playerHealth.OnEntityDamaged += PlayShaking;

    private void OnDisable() => playerHealth.OnEntityDamaged -= PlayShaking;

    public void PlayShaking(Health health, int damage) => StartCoroutine(Shake());

    private IEnumerator Shake()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * Mathf.Lerp(shakeMagnitude, 0f, elapsedTime / shakeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = initialPosition;
    }
}
