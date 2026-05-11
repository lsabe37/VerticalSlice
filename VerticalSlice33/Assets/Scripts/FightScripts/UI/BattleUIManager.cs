using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    public GameObject bloodyBorder;

    private void Start()
    {
        BossLocator.Instance.boss.nightmareMode += activateBorder;
        BossLocator.Instance.boss.endNightmare += hideBorder;
    }

    private void activateBorder()
    {
        bloodyBorder.SetActive(true);
    }

    private void hideBorder()
    {
        bloodyBorder.SetActive(false);
    }
}
