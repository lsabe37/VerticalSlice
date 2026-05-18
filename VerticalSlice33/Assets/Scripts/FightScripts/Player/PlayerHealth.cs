using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 50f;
    public float maxHealth = 50f;
    private bool hit = false;
    public GameObject parrySparks;

    private void Update()
    {
        if (hit == true)
        {
            TakeDamage(5);
        }
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);

        hit = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("EnemyAtk") || collider.gameObject.CompareTag("EnemyProjectile"))
        {

            if (PlayerLocator.Instance.player.parrystance == false)
            {
                hit = true;
                Debug.Log("hit by enemy!");

                PlayerLocator.Instance.player.FlashWhite();
            }

            if (PlayerLocator.Instance.player.parrystance == true)
            {
                performParry();
            }
        }
    }

    public void performParry()
    {
        Instantiate(parrySparks, transform.position, Quaternion.identity);
    }
}
