using System.Collections;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance { get; private set; }

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
    }

    public IEnumerator FadeOut(in float fadeDur) => Fade(1f, fadeDur);
    public IEnumerator FadeIn(in float fadeDur) => Fade(0f, fadeDur);

    private IEnumerator Fade(float targetAlpha, float fadeDuration)
    {
        canvasGroup.blocksRaycasts = true;
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        canvasGroup.blocksRaycasts = targetAlpha > 0f;
    }
}
