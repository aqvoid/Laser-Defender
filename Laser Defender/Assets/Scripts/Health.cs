using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int healthPoints;
    [SerializeField] private bool isPlayer;

    public static event Action OnAnybodyDamaged;
    public event Action OnPlayerDamaged;

    public static event Action OnPlayerDeath;
    public static event Action<Health> OnEnemyDeath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (damageDealer == null) return;

        int damage = damageDealer.GetDamage();
        TakeDamage(damage);
    }

    private void TakeDamage(int damage)
    {
        OnPlayerDamaged?.Invoke();
        OnAnybodyDamaged?.Invoke();
        healthPoints -= damage;

        if (healthPoints <= 0) Die();
    }

    private void Die()
    {
        if (isPlayer)
            OnPlayerDeath?.Invoke();
        else
            OnEnemyDeath?.Invoke(this);
        Destroy(gameObject);
    }

    public int GetHealth() => healthPoints;
}
