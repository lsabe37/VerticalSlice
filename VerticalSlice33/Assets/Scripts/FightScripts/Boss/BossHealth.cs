using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossHealth : MonoBehaviour
{
    public float health = 200f;
    public float maxHealth = 200f;
    [HideInInspector] public float healthRatio;

    public static event Action OnBossDeath;
    private bool notDead = true;

    private void Start()
    {
        health = 5f;
        notDead = true;
    }

    private void Update()
    {
        healthRatio = health / maxHealth;

        if(health <= 0 && notDead == true)
        {
            OnBossDeath?.Invoke();
            notDead = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerAtk"))
        {
            health -= 5f;
            Destroy(collider.gameObject);
            Debug.Log("Damaged boss!");

            BossLocator.Instance.boss.FlashWhite();
        }
    }
}
