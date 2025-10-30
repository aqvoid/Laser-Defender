using System.Collections;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [Header("=== General ===")]
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float projectileLifetime;
    [SerializeField] protected float projectileAttackRate;
    protected bool canAttack = true;

    protected abstract void StartAttack();
    protected abstract void StopAttack();

    protected virtual void ShootProjectile(Vector2 projectileDirection)
    {
        GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRb = instance.GetComponent<Rigidbody2D>();

        if (projectileRb != null)
            projectileRb.linearVelocity = projectileDirection * projectileSpeed;

        Destroy(instance, projectileLifetime);
    }
    protected IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(projectileAttackRate);
        canAttack = true;
    }
}