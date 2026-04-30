using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazards : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float health = PlayerLocator.Instance.playerHealth.health -= 5f;
            Debug.Log("player is: " + health.ToString());
        }
    }
}
