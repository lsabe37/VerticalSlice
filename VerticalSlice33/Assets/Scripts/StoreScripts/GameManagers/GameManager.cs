using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    public CanvasGroup[] actionUI;
    public CanvasGroup navigationUI;
    public CanvasGroup abilityUI;
    public CanvasGroup gunUI;
    public CanvasGroup idUI;
    public CanvasGroup responseBox;
    private bool UiIsVisible = false;
    public bool gunOut = false;
    public bool idOut = false;
    public bool wasShot = false;
    public bool lookingAtCustomer = true;
    public GameObject gunExplosion;

    [Header("View Changer")]
    public Transform donutLocation;
    public Transform tvLocation;
    [SerializeField] GameObject cam;
    public UnityEngine.UI.Image fadeImage;
    public float fadeDuration = 1.0f;
    private bool dimming;

    [Header("Donut")]
    public int SelectedDonutID;

    [Header("Bg")]
    public SpriteRenderer bg;
    public SpriteRenderer table;
    public bool lightsOff;

    public delegate void lightsOffEvent();
    public event lightsOffEvent lightsTurnOff;

    public delegate void shootEvent();
    public event shootEvent shootGun;

    public delegate void switchScreen();
    public event switchScreen screenSwitch;

    private void Start()
    {
        DisableActionUI();
        DisableNavigationUI();
    }

    private void Update()
    {
        if (gunOut == true && Input.GetKeyDown(KeyCode.S) && lookingAtCustomer == true && Locator.Instance.customerManager.customerPresent == true)
        {
            BlastGun();
            shootGun();
        }

        if(Locator.Instance.dialogueUI.isTalking == true || Locator.Instance.customerManager.customerPresent == false) DisableAbilityUI();
        else if(Locator.Instance.dialogueUI.isTalking == false && Locator.Instance.customerManager.customerPresent == true && lookingAtCustomer == true) EnableAbilityUI();
    }


    private void HideActionUI()
    {
        foreach (CanvasGroup canGroup in actionUI)
        {
            canGroup.alpha = 0f;
            canGroup.blocksRaycasts = false;
            canGroup.interactable = false;
        }

    }
    private void ShowActionUI()
    {
        foreach (CanvasGroup canGroup in actionUI)
        {
            canGroup.alpha = 1f;
            canGroup.blocksRaycasts = true;
            canGroup.interactable = true;
        }
    }

    public void DisableActionUI()
    {
        foreach (CanvasGroup canGroup in actionUI)
        {
            canGroup.blocksRaycasts = false;
            canGroup.interactable = false;
        }
    }
    public void EnableActionUI()
    {
        foreach (CanvasGroup canGroup in actionUI)
        {
            canGroup.blocksRaycasts = true;
            canGroup.interactable = true;
        }
    }

    public void DisableNavigationUI()
    {
        navigationUI.blocksRaycasts = false;
        navigationUI.interactable = false;
    }
    public void EnableNavigationUI()
    {
        navigationUI.blocksRaycasts = true;
        navigationUI.interactable = true;
    }

    public void DisableAbilityUI()
    {
        abilityUI.blocksRaycasts = false;
        abilityUI.interactable = false;
    }
    public void EnableAbilityUI()
    {
        abilityUI.blocksRaycasts = true;
        abilityUI.interactable = true;
    }

    public void DisableRepsonseUI()
    {
        responseBox.alpha = 0f;
        responseBox.blocksRaycasts = false;
        responseBox.interactable = false;
    }
    public void EnableResponseUI()
    {
        responseBox.alpha = 1f;
        responseBox.blocksRaycasts = true;
        responseBox.interactable = true;
    }


    public void useGun()
    {
        gunOut = !gunOut;

        if (gunOut == true)
        {
            gunUI.alpha = 1f;
        }
        else
        {
            gunUI.alpha = 0f;
        }
    }
    private void BlastGun()
    {
        GameObject explosion = Instantiate(gunExplosion, transform.position, Quaternion.identity);
        Destroy(explosion, .2f);
        useGun();
        wasShot = true;
        Locator.Instance.customerManager.customerShotReact();
    }

    public void useLights()
    {
        lightsOff = !lightsOff;

        if (lightsOff == true)
        {
            turnOffLights();
        }
        else
        {
            resetBg();
        }

        lightsTurnOff();
    }


    public void viewID()
    {
        idOut = !idOut;

        if (idOut == true)
        {
            idUI.alpha = 1f;
        }
        else
        {
            idUI.alpha = 0f;
        }
    }



    // Bg changer logic
    public void changeBg()
    {
        table.color = new Color(1f, 0f, 0f, 1f);
        bg.color = new Color(1f, 0f, 0f, 1f);
    }

    public void turnOffLights()
    {
        table.color = new Color(0.3f, 0.3f, 0.4f, 1f);
        bg.color = new Color(0.2f, 0.2f, 0.3f, 1f);
    }

    public void resetBg()
    {
        table.color = new Color(1f, 1f, 1f, 1f);
        bg.color = new Color(1f, 1f, 1f, 1f);
    }

    // view changer logic
    public void SwitchToDonuts()
    {
        FadeToBlack();
        Invoke("viewDonuts", fadeDuration);
        Invoke("FadeFromBlack", fadeDuration);
    }

    public void SwitchToCustomers()
    {
        FadeToBlack();
        Invoke("viewCustomer", fadeDuration);
        Invoke("FadeFromBlack", fadeDuration);
    }

    public void SwitchToTV()
    {
        FadeToBlack();
        Invoke("viewTv", fadeDuration);
        Invoke("FadeFromBlack", fadeDuration);
    }

    private void viewDonuts()
    {
        cam.transform.position = new Vector3(donutLocation.position.x, donutLocation.position.y, -5f);
        lookingAtCustomer = false;

        if (gunOut == true)
        {
            useGun();
        }

        HideActionUI();
    }

    private void viewCustomer()
    {
        cam.transform.position = new Vector3(0f, 0f, -5f);
        lookingAtCustomer = true;
        ShowActionUI();
    }

    private void viewTv()
    {
        cam.transform.position = new Vector3(tvLocation.position.x, tvLocation.position.y, -5f);
        lookingAtCustomer = false;

        if (gunOut == true)
        {
            useGun();
        }

        HideActionUI();
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
