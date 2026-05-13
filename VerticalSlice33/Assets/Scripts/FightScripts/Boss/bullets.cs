using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullets : MonoBehaviour
{
    public float bulletDuration = 1f;

    public float rotation = 0f;
    public float speed = 1f;

    private Vector2 spawnPoint;
    private float timer = 0f;

    public bool destructable;
    [SerializeField] private float bulletHP;
    [SerializeField] private bool pause;
    [SerializeField] private Transform target;
    [SerializeField] private Animator anim;


    private void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > bulletDuration)
            Destroy(this.gameObject);

        transform.position = Movement(timer);
    }

    private Vector2 Movement(float timer)
    {
        float x = -timer * speed * transform.right.x;
        float y = -timer * speed * transform.right.y;
        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ground"))
        {
            //anim.SetBool("explode", true);
        }
    }

    private void destroyProjectile()
    {
        Destroy(gameObject);
    }
}
