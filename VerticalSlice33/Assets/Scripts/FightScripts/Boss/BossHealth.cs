using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossHealth : MonoBehaviour
{
    public float health = 200f;
    public float maxHealth = 200f;
    [HideInInspector] public float healthRatio;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        healthRatio = health / maxHealth;
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
