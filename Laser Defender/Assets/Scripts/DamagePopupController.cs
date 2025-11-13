using UnityEngine;

public class DamagePopupController : MonoBehaviour
{
    private DamagePopupPooling pool;
    private Health entityHealth;

    private void Awake()
    {
        pool = GameObject.FindGameObjectWithTag("Popup Pool").GetComponent<DamagePopupPooling>();
        entityHealth = GetComponent<Health>();
    }

    private void OnEnable() => entityHealth.OnEntityDamaged += SpawnPopup;
    private void OnDisable() => entityHealth.OnEntityDamaged -= SpawnPopup;

    private void SpawnPopup(Health entityHealth, int damage)
    {
        GameObject popup = pool.GetPopupFromPool();
        popup.transform.position = entityHealth.transform.position;

        if (entityHealth.gameObject.CompareTag("Player")) popup.transform.localScale = Vector2.one * 1.5f;
        else popup.transform.localScale = Vector2.one;

        popup.GetComponent<DamagePopup>().Animate(damage, pool);
    }
}
