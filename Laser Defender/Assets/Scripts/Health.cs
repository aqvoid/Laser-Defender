using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int healthPoints;
    [SerializeField] private EntityType entityType;

    private enum EntityType { Player, Enemy };

    private int maxHealthPoints = 100;

    public static event Action OnAnybodyDamaged;
    public event Action OnPlayerDamaged;
    public event Action OnEnemyDamaged;

    public static event Action OnPlayerDeath;
    public static event Action<Health> OnEnemyDeath;

    private void Awake() => maxHealthPoints = healthPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (damageDealer == null) return;
        else if (damageDealer.CompareTag("Projectile")) Destroy(damageDealer.gameObject);

        int damage = damageDealer.GetDamage();
        TakeDamage(damage);
    }

    private void TakeDamage(int damage)
    {
        healthPoints -= damage;

        switch (entityType)
        {
            case EntityType.Player:
                OnPlayerDamaged?.Invoke();
                break;
            case EntityType.Enemy:
                OnEnemyDamaged?.Invoke();
                break;
        }

        OnAnybodyDamaged?.Invoke();

        if (healthPoints <= 0) Die();
    }

    private void Die()
    {
        switch (entityType)
        {
            case EntityType.Player:
                OnPlayerDeath?.Invoke();
                break;
            case EntityType.Enemy:
                OnEnemyDeath?.Invoke(this);
                break;
        }
        Destroy(gameObject);
    }

    public int GetHealth() => healthPoints;
    public int GetMaxHealth() => maxHealthPoints;
}
