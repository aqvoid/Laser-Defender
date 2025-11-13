using UnityEngine;

public class DamagePopupController : MonoBehaviour
{
    [SerializeField] private GameObject popup;

    private Health entityHealth;
    private GameObject popupParent;

    private void Awake()
    {
        entityHealth = GetComponent<Health>();
        popupParent = GameObject.FindGameObjectWithTag("Game Canvas");
    }

    private void OnEnable()
    {
        entityHealth.OnEntityDamaged += SpawnPopup;
    }
    private void OnDisable()
    {
        entityHealth.OnEntityDamaged -= SpawnPopup;
    }

    private void SpawnPopup(Health entityHealth, int damage)
    {
        GameObject prefab = Instantiate(popup, entityHealth.transform.position, Quaternion.identity, popupParent.transform);

        prefab.GetComponent<DamagePopup>().Animate(damage);
    }
}
