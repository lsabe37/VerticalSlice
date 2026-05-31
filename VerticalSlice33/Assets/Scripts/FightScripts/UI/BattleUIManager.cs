using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    public GameObject bloodyBorder;
    public GameObject gameOverScreen;

    [SerializeField] private GameObject[] tutorial;

    private void Start()
    {
        BossLocator.Instance.boss.nightmareMode += activateBorder;
        BossLocator.Instance.boss.endNightmare += hideBorder;

        PlayerLocator.Instance.playerHealth.lose += gameOver;
        BossLocator.Instance.boss.onEndTutorial += EndTutorial;

        gameOverScreen.SetActive(false);

        StartCoroutine(showTutorial());
    }

    private IEnumerator showTutorial()
    {
        yield return new WaitForSeconds(3f);

        for(int i = 0; i < tutorial.Length; i++)
        {
            tutorial[i].SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }

    private void EndTutorial()
    {
        for (int i = 0; i < tutorial.Length; i++)
        {
            tutorial[i].SetActive(false);
        }
    }

    private void activateBorder()
    {
        bloodyBorder.SetActive(true);
    }

    private void hideBorder()
    {
        bloodyBorder.SetActive(false);
    }

    private void gameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
