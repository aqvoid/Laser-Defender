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

        float rndAngle = Random.Range(-10f, 10f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
            tmp.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            transform.position = Vector2.Lerp(startPos, endPos, t);
            transform.rotation = Quaternion.Euler(0f, 0f, rndAngle);
            yield return null;
        }

        pool.ReturnPopupToPool(gameObject);
        tmp.alpha = startAlpha;
    }
}
