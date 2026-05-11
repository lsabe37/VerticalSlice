using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgManager : MonoBehaviour
{
    public SpriteRenderer[] bg;
    public GameObject nightmareBg;

    private void Start()
    {
        BossLocator.Instance.boss.nightmareMode += changeBg;
        BossLocator.Instance.boss.endNightmare += resetBg;
    }

    private void changeBg()
    {
        for (int i = 0; i < bg.Length; i++)
        {
            bg[i].color = new Color(0.5f, 1f, 0f, 1f);
        }

        nightmareBg.SetActive(true);
    }

    private void resetBg()
    {
        for (int i = 0; i < bg.Length; i++)
        {
            bg[i].color = new Color(1f, 1f, 1f, 1f);
        }

        nightmareBg.SetActive(false);
    }
}
