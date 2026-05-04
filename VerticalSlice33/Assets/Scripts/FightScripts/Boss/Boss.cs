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
    [SerializeField] private Player player;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private Transform angelPoint;
    private Vector2 currentDirection = Vector2.right;
    private int activeAttack;
    private int selectedSign;
    public float speed = 10f;
    private bool chase = false;
    private bool flipped = false;

    [Header("Attacks")]
    [SerializeField] private GameObject[] signsOptions;
    [SerializeField] private GameObject flamePillars;
    [SerializeField] private GameObject lightningBolt;
    [SerializeField] private GameObject anvil;
    [SerializeField] private GameObject splashEffect;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject shooter;
    [SerializeField] private projectileSpawner[] supportShooter;
    [SerializeField] private GameObject fakeScarecrow;

    [Header("Effects")]
    [SerializeField] private GameObject ripples;
    [SerializeField] private GameObject meleeBlast;

    [Header("AttackPoints")]
    [SerializeField] private Transform meleeAForward;
    [SerializeField] private Transform meleeAReverse;
    [SerializeField] private Transform[] scarecrowTP;

    public bool currentlyInAction;


    private void Start()
    {

    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 5f)
        {
            bossDistance = BossDistanceState.Close;
        }
        else if (Vector2.Distance(transform.position, player.transform.position) > 5f && Vector2.Distance(transform.position, player.transform.position) < 10f)
        {
            bossDistance = BossDistanceState.Mid;
        }
        else
        {
            bossDistance = BossDistanceState.Far;
        }

        if (currentlyInAction == false)
        {
            ResetToIdle();
            currentlyInAction = true;
        }

    }

    private void FixedUpdate()
    {
        if (chase == true)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > 4f)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                FaceTarget();
            }
            if (distance <= 4f)
            {
                chase = false;
                anim.SetBool("chase", false);
            }
        }
    }

    private void ResetToIdle()
    {
        anim.SetTrigger("idle");
    }

    private void RestartAttackPattern()
    {
        switch (bossDistance)
        {
            case BossDistanceState.Close:
                PassiveState();
                break;
            case BossDistanceState.Mid:
                PassiveState();
                break;
            case BossDistanceState.Far:
                PassiveState();
                break;
        }
    }

    private void RangedBehavior()
    {
        //move towards player or shoot projectile
    }

    private void PassiveState()
    {
        //play one of three idle anims (fast, medium, slow)
        //Select attack action upon complete
        int actionChoice = Random.Range(0, 3);

        if (actionChoice == 0)
        {
            FastCharge();
        }
        else if (actionChoice == 1)
        {
            MediumCharge();
        }
        else if (actionChoice == 2)
        {
            SlowCharge();
        }
    }

    private void FastCharge()
    {
        anim.SetTrigger("fastCharge");
    }
    private void MediumCharge()
    {
        anim.SetTrigger("midCharge");
    }
    private void SlowCharge()
    {
        anim.SetBool("chase", true);
        chase = true;
    }

    private void postFastChargeAttack()
    {
        Debug.Log("fast attack!");

        int actionChoice = Random.Range(0, 3);
        if (actionChoice == 0)
        {
            anim.SetTrigger("select");
        }
        else if (actionChoice == 1)
        {
            anim.SetTrigger("jump");
        }
        else
        {
            anim.SetTrigger("splash");
        }

    }

    private void postMediumChargeAttack()
    {
        Debug.Log("medium attack!");

        int actionChoice = Random.Range(0, 3);
        if (actionChoice == 0)
        {
            anim.SetTrigger("meleeA");
        }
        else if (actionChoice == 1)
        {
            anim.SetTrigger("meleeB");
        }
        else
        {
            anim.SetTrigger("meleeB");
        }
    }

    private void postSlowChargeAttack()
    {
        Debug.Log("slow attack!");

        int actionChoice = Random.Range(0, 3);
        if (actionChoice == 0)
        {
            anim.SetBool("chase", true);
        }
        else if (actionChoice == 1)
        {
            anim.SetBool("chase", true);
        }
        else
        {
            anim.SetBool("chase", true);
        }
    }



    // Select sign logic
    private void endSelect()
    {
        UseAttack(selectedSign);
        currentlyInAction = false;
    }

    private void selectSign()
    {
        int randomSign = Random.Range(0, signsOptions.Length);
        GameObject signInstance = Instantiate(signsOptions[randomSign], new Vector2(transform.position.x + 3f, transform.position.y + 4f), Quaternion.identity);

        Signs signSelected = signInstance.GetComponent<Signs>();
        selectedSign = signSelected.SignID;

        Destroy(signInstance, .75f);
    }

    private void UseAttack(int chosenSign)
    {
        switch (chosenSign)
        {
            case 1:
                StartCoroutine(flameAttack());
                break;
            case 2:
                StartCoroutine(lightningAttack()); ;
                break;
            case 3:
                anvilAttack();
                break;
        }

    }

    // flame attack
    private IEnumerator flameAttack()
    {
        for (int i = 7; i < 35; i += 7)
        {
            Instantiate(flamePillars, new Vector2(transform.position.x + i, -5), Quaternion.identity);
            Instantiate(flamePillars, new Vector2(transform.position.x - i, -5), Quaternion.identity);
            yield return new WaitForSeconds(.3f);
        }

    }

    // lightning attack
    private IEnumerator lightningAttack()
    {
        for (int i = 4; i < 20; i += 4)
        {
            Instantiate(lightningBolt, new Vector2(transform.position.x + i, -5), Quaternion.identity);
            Instantiate(lightningBolt, new Vector2(transform.position.x - i, -5), Quaternion.identity);
            yield return new WaitForSeconds(.2f);
        }
    }

    //anvil attack
    private void anvilAttack()
    {
        Instantiate(anvil, new Vector2(player.transform.position.x, 15), Quaternion.identity);
    }

    private void scarecrowsAttack()
    {
        anim.SetTrigger("scarecrow");

        int location = Random.Range(0, scarecrowTP.Length);
        transform.position = scarecrowTP[location].position;

        for (int i = 0; i < scarecrowTP.Length; i++)
        {
            if (i == location) continue;
            Instantiate(fakeScarecrow, scarecrowTP[i].position, Quaternion.identity);

        }
    }

    private void scarecrowExplosion()
    {
        Instantiate(explosion, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }

    // dash behind player during melee combo
    private void meleeDash()
    {
        Vector2 playerPosition = player.transform.position;
        if (transform.localScale.x > 0)
        {
            Vector2 newPosition = new Vector2(playerPosition.x + distanceFromPlayer, transform.position.y + 1f);
            transform.position = newPosition;
        }
        else
        {
            Vector2 newPosition = new Vector2(playerPosition.x - distanceFromPlayer, transform.position.y);
            transform.position = newPosition;
        }
    }

    private void teleportAttack()
    {
        Vector2 tpLocation = new Vector2(Random.Range(-10, 10), Random.Range(3, 7));
        transform.position = tpLocation;
        rb.gravityScale = 0f;
        rb.velocity = Vector3.zero;
        shooter.SetActive(true);
    }

    // enables selection of new passive action
    public void EndCurrentAttack()
    {
        Debug.Log("attack ended");
        currentlyInAction = false;
    }


    // move towards player
    public void chasePlayer()
    {
        chase = true;
    }

    //stop moving towards player
    public void stopChasePlayer()
    {
        chase = false;
    }


    public void GravityOn()
    {
        rb.gravityScale = 1f;
    }

    private void LaunchUp()
    {
        rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        Instantiate(splashEffect, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }

    private void TpToPlayer()
    {
        transform.position = new Vector2(player.transform.position.x, transform.position.y);
    }

    private void TPtoCenter()
    {
        Vector2 tpLocation = new Vector2(Random.Range(-10, 10), Random.Range(3, 7));
        transform.position = tpLocation;
        rb.velocity = Vector3.zero;
        shooter.SetActive(true);
    }

    private void createRipples()
    {
        Instantiate(ripples, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }

    private void meleeAForwardBlast()
    {
        GameObject instance = Instantiate(meleeBlast, new Vector2(meleeAForward.position.x, meleeAForward.position.y), Quaternion.identity);
        if (flipped == false)
        {
            instance.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void meleeAReverseBlast()
    {
        GameObject instance = Instantiate(meleeBlast, new Vector2(meleeAReverse.position.x, meleeAForward.position.y), Quaternion.identity);
        if (flipped == true)
        {
            instance.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void shootBullet()
    {
        for (int i = 0; i < supportShooter.Length; i++)
        {
            supportShooter[i].ShootSpread();
        }
    }

    //flips to face player
    private void FaceTarget()
    {
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            flipped = true;
        }

        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            flipped = false;
        }
    }

    private void jumpBack()
    {
        float backwardDirection = transform.localScale.x > 0 ? -1f : 1f;

        Vector2 jumpVector = new Vector2(backwardDirection * 10, 7);
        rb.AddForce(jumpVector, ForceMode2D.Impulse);
    }

}
