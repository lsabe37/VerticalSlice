using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileSpawner : MonoBehaviour
{
    enum SpreadType { Spin, Straight, DownOnly }
    enum SpawnType { Normal, Surround }

    [Header("Bullet Attributes")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public int numberOfBullets = 3;

    [Header("Shooter Attributes")]
    public Transform firePoint;
    public float spreadAngle = 30f;
    private float timer = 0f;
    private float startAngle;
    [SerializeField] private float firingRate = 1f;
    [SerializeField] private float rotateDirection = 1f;
    [SerializeField] private SpreadType spreadType;
    [SerializeField] private SpawnType spawnType;
    [SerializeField] private float offset = 0f;
    public float radius = 2f;

    private float timesFired;
    [SerializeField] private float maxFireCount;

    public Transform target;

    public bool onBoss;
    public bool onAngel;


    void Start()
    {
        startAngle = -spreadAngle / 2f + offset;
        GameObject playerTarget = GameObject.FindWithTag("Player");
        target = playerTarget.transform;

        //BossHealth.OnBossDeath += DestroySun;
    }

    private void Update()
    {
        if (timesFired >= maxFireCount && onBoss == false)
        {
            Destroy(gameObject);
            BossLocator.Instance.boss.activeSun = false;
        }
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (spreadType == SpreadType.Spin)
        {
            startAngle = startAngle + 1;
        }

        if (spreadType == SpreadType.Straight)
        {
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            float angleRad = Mathf.Atan2(-directionToTarget.y, -directionToTarget.x);
            float angleDeg = angleRad * Mathf.Rad2Deg;
            startAngle = angleDeg - spreadAngle / 2;
        }

        if (timesFired < maxFireCount)
        {
            shoot();
        }

        if (onAngel && Vector2.Distance(transform.position, target.position) <= 10f)
        {
            shoot();
        }
    }

    public void shoot()
    {
        if (timer >= firingRate)
        {
            ShootSpread();
            timesFired += 1;
            timer = 0;
        }
    }


    public void ShootSpread()
    {
        startAngle = startAngle + rotateDirection;
        float angleStep = spreadAngle / (numberOfBullets - 1);

        for (int i = 0; i < numberOfBullets; i++)
        {
            if (spawnType == SpawnType.Normal)
            {
                float currentAngle = startAngle + (i * angleStep);
                Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    Vector2 direction = rotation * Vector2.right;
                    rb.velocity = direction * bulletSpeed;
                }
            }

            if (spawnType == SpawnType.Surround)
            {
                float currentAngle = startAngle + (i * angleStep);
                float radians = currentAngle * Mathf.Deg2Rad;

                float xOffset = Mathf.Cos(radians) * radius;
                float yOffset = Mathf.Sin(radians) * radius;

                Vector2 spawnPosition = (Vector2)target.position + new Vector2(xOffset, yOffset);

                Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);
                GameObject bullet = Instantiate(bulletPrefab, spawnPosition, rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    Vector2 direction = rotation * Vector2.right;
                    rb.velocity = direction * bulletSpeed;
                }
            }

        }

    }

    private void DestroySun()
    {
        Destroy(gameObject);
    }
}