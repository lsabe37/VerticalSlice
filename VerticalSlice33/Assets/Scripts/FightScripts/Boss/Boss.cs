using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum BossDistanceState { Close, Mid, Far }
    public BossDistanceState bossDistance;

    [Header("Context")]
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    [SerializeField] private Player player;
    [SerializeField] private float distanceFromPlayer;
    public float speed = 10f;
    private bool chase = false;

    public bool currentlyInAction;

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 5f)
        {
            bossDistance = BossDistanceState.Close;
        }
        else if (Vector2.Distance(transform.position, player.transform.position) >= 5f && Vector2.Distance(transform.position, player.transform.position) < 10f)
        {
            bossDistance = BossDistanceState.Mid;
        }
        else
        {
            bossDistance = BossDistanceState.Far;
        }

        RestartAttackPattern();
    }

    private void FixedUpdate()
    {
        if (chase == true)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > 4f)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            if (distance <= 4f)
            {
                chase = false;
            }
        }
    }

    private void RestartAttackPattern()
    {
        switch (bossDistance)
        {
            case BossDistanceState.Close:
                spriteRenderer.color = new Color(1f, 0.5f, 0f, 1f);
                chase = false;
                break;
            case BossDistanceState.Mid:
                spriteRenderer.color = new Color(0f, 0.5f, 1f, 1f);
                break;
            case BossDistanceState.Far:
                spriteRenderer.color = new Color(0.5f, 1.0f, 0f, 1f);
                chase = true;
                break;
        }
    }
}
