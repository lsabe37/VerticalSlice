using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    public GameObject bloodyBorder;
    public GameObject gameOverScreen;

    private void Start()
    {
        BossLocator.Instance.boss.nightmareMode += activateBorder;
        BossLocator.Instance.boss.endNightmare += hideBorder;

        PlayerLocator.Instance.playerHealth.lose += gameOver;

        gameOverScreen.SetActive(false);
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
