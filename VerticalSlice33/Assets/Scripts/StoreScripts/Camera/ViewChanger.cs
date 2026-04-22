using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ViewChanger : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2.0f;
    private bool dimming;

    public void SwitchToTv()
    {
        FadeToBlack();
        Invoke("Locator.Instance.gameManager.viewDonuts", 2f);
        Invoke("FadeFromBlack", 2f);
    }

    private void FadeToBlack()
    {
        StartCoroutine(FadeRoutine(0f, 1f));
    }

    private void FadeFromBlack()
    {
        StartCoroutine(FadeRoutine(1f, 0f));
    }

    private IEnumerator FadeRoutine(float startAlpha, float endAlpha)
    {
        float timer = 0f;
        if (!fadeImage.gameObject.activeInHierarchy)
        {
            fadeImage.gameObject.SetActive(true);
        }

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
            yield return null;
        }

        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, endAlpha);

        if (endAlpha == 0f)
        {
            fadeImage.gameObject.SetActive(false);
        }
    }
}
