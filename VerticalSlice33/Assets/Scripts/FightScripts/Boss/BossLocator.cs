using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLocator : MonoBehaviour
{
    public static BossLocator Instance { get; private set; }
    public Boss boss { get; private set; }
    public BossHealth bossHealth { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        GameObject bossObject = GameObject.FindWithTag("Boss");
        boss = bossObject.GetComponent<Boss>();
        bossHealth = bossObject.GetComponent<BossHealth>();
    }
}
