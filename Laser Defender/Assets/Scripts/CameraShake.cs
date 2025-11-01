using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeMagnitude;

    [Header("=== References ===")]
    [SerializeField] private Health playerHealth;

    private Vector3 initialPosition;


    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnEnable() => playerHealth.OnPlayerDamaged += PlayShaking;

    private void OnDisable() => playerHealth.OnPlayerDamaged -= PlayShaking;

    public void PlayShaking() => StartCoroutine(Shake());

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
