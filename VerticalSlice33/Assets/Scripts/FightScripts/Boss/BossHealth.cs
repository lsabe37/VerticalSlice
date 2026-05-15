using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float health = 200f;
    public float maxHealth = 200f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerAtk"))
        {
            health -= 5f;
            Destroy(collider.gameObject);
            Debug.Log("Damaged boss!");
        }
    }
}
