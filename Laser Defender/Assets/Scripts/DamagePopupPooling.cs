using System.Collections.Generic;
using UnityEngine;

public class DamagePopupPooling : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private int initialSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
            AddPopupToPool();
    }

    public GameObject GetPopupFromPool()
    {
        if (pool.Count == 0) AddPopupToPool();
        
        GameObject popup = pool.Dequeue();
        popup.SetActive(true);
        return popup;
    }

    public void ReturnPopupToPool(GameObject popup)
    {
        popup.SetActive(false);
        pool.Enqueue(popup);
    }

    public void AddPopupToPool()
    {
        GameObject popup = Instantiate(popupPrefab, transform);
        popup.SetActive(false);
        pool.Enqueue(popup);
    }
}
