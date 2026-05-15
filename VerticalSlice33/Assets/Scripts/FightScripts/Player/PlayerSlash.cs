using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    public CapsuleCollider2D collider;
    public float speed = 1f;

    public float bulletDuration = 1f;

    private Vector2 spawnPoint;
    private float timer = 0f;

    private float flipped = -1f;

    private void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
        Physics2D.IgnoreLayerCollision(7, 9, true);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > bulletDuration)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
