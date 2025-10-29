using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifetime;
    [SerializeField] private float attackRate;

    private PlayerInput playerInput;
    private InputAction attackAction;

    private Coroutine attackCoroutine;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        attackAction = playerInput.actions["Attack"];
        attackAction.started += OnAttackStarted;
        attackAction.canceled += OnAttackCanceled;
    }

    private void OnDisable()
    {
        attackAction.started -= OnAttackStarted;
        attackAction.canceled -= OnAttackCanceled;
    }

    private void OnAttackStarted(InputAction.CallbackContext ctx)
    {
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackContinuously());
        }
    }

    private void OnAttackCanceled(InputAction.CallbackContext ctx)
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    private IEnumerator AttackContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D projectileRb = instance.GetComponent<Rigidbody2D>();
            if (projectileRb != null) projectileRb.linearVelocity = transform.up * projectileSpeed;

            Destroy(instance, projectileLifetime);
            yield return new WaitForSeconds(attackRate);
        }
    }
}
