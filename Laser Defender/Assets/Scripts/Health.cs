using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int healthPoints;

    public static event Action OnAnybodyDamaged;
    public event Action OnPlayerDamaged;

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

        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
