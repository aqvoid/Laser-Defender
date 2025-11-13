using TMPro;
using UnityEngine;
using System.Collections;

public class DamagePopup : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    private void Awake() => tmp = GetComponent<TextMeshProUGUI>();

    public void Animate(int damage)
    {
        tmp.text = damage.ToString();
        StartCoroutine(PopupAnimationOut());
    }

    private IEnumerator PopupAnimationOut()
    {
        float elapsed = 0f;
        float duration = 1f;

        float startAlpha = tmp.alpha;
        float targetAlpha = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            tmp.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            yield return null;
        }

        Destroy(tmp.gameObject);
    }
}
