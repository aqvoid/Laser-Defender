using TMPro;
using UnityEngine;
using System.Collections;

public class DamagePopup : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    private DamagePopupPooling pool;

    private void Awake() => tmp = GetComponent<TextMeshProUGUI>();

    public void Animate(int damage, DamagePopupPooling pool)
    {
        this.pool = pool;
        tmp.text = "-" + damage.ToString();
        StartCoroutine(PopupAnimationOut());
    }

    private IEnumerator PopupAnimationOut()
    {
        float elapsed = 0f;
        float duration = 1f;

        float startAlpha = tmp.alpha;
        float targetAlpha = 0f;

        Vector2 startPos = transform.position;
        Vector2 endPos = startPos + Vector2.up; 

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            tmp.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            yield return null;
        }

        pool.ReturnPopupToPool(gameObject);
        tmp.alpha = startAlpha;
    }
}
