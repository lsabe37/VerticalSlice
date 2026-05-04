using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLogic : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private float startDelay;
    [SerializeField] private CapsuleCollider2D collider;
    [SerializeField] private GameObject effect;

    private void startAttack()
    {
        Invoke(nameof(StartUp), startDelay);
    }

    private void StartUp()
    {
        anim.SetBool("start", true);
    }

    public void endAttack()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void activateHitbox()
    {
        collider.enabled = true;
    }

    private void createEffect()
    {
        Instantiate(effect, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }
}
