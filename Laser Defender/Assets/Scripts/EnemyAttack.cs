using System.Collections;
using UnityEngine;

public class EnemyAttack : Attack
{
    [Header("=== Enemy Projectile Settings ===")]
    [SerializeField] private float minAttackRate;
    [SerializeField] private float attackRateVariance;

    private void Start()
    {
        StartAttack();
    }

    protected override void StartAttack()
    {
        StartCoroutine(AttackLoop());
    }

    protected override void StopAttack()
    {
        StopAllCoroutines();
    }

    private IEnumerator AttackLoop()
    {
        while (true)
        {
            if (canAttack)
            {
                ShootProjectile(-transform.up);
                StartCoroutine(AttackCooldown());
            }

            float randomRate = Random.Range(projectileAttackRate - attackRateVariance, projectileAttackRate + attackRateVariance);
            randomRate = Mathf.Clamp(randomRate, minAttackRate, float.MaxValue);

            yield return new WaitForSeconds(randomRate);
        }
    }
 }
