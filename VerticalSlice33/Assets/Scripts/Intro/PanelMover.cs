using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelMover : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    private int currentPanel;
    [SerializeField] private Button nextButton;

    [Header("FadeToBlack")]
    public UnityEngine.UI.Image fadeImage;
    public float fadeDuration = 1.0f;
    private bool dimming;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            nextButton.onClick.Invoke();
        }
    }

    public void NextPanel()
    {
        if (currentPanel < panels.Length - 1)
        {
            StartCoroutine(NextPage());
        }

        if (currentPanel >= panels.Length - 1)
        {
            StartCoroutine(FadeAndLoad());
        }
    }

    private IEnumerator NextPage()
    {
        panels[currentPanel].SetActive(false);
        currentPanel += 1;
        panels[currentPanel].SetActive(true);

        yield return null;
    }

    private IEnumerator FadeAndLoad()
    {
        StartCoroutine(FadeRoutine(0f, 1f));
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Store");
    }

    private void FadeToBlack()
    {
        
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
