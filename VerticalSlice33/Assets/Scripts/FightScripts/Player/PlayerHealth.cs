using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 50f;
    public float maxHealth = 50f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("EnemyAtk") || collider.gameObject.CompareTag("EnemyProjectile"))
        {
            health -= 5f;
            Debug.Log("hit by enemy!");
        }
    }

}
