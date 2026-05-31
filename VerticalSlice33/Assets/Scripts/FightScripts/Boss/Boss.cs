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
    private SpriteRenderer sr;
    [SerializeField] private Player player;

    [Header("Status")]
    public bool beginFight = false;

    [SerializeField] private float distanceFromPlayer;
    private Vector2 currentDirection = Vector2.right;
    private int activeAttack;
    private int selectedSign;
    public float speed = 10f;
    private bool chase = false;
    private bool flipped = false;
    [HideInInspector] public bool currentlyInAction;
    [HideInInspector] public bool activeSun;

    [Header("Attacks")]
    [SerializeField] private GameObject[] signsOptions;
    [SerializeField] private GameObject sunSpirit;
    [SerializeField] private GameObject lightningBolt;
    [SerializeField] private GameObject anvil;
    [SerializeField] private GameObject splashEffect;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject shooter;
    [SerializeField] private GameObject fakeScarecrow;
    [SerializeField] private GameObject tentacleSign;
    [SerializeField] private GameObject tentacleSpear;
    [SerializeField] private GameObject miniAngel;

    [Header("Effects")]
    [SerializeField] private GameObject ripples;
    [SerializeField] private GameObject meleeBlast;

    [Header("AttackPoints")]
    [SerializeField] private Transform meleeAForward;
    [SerializeField] private Transform meleeAReverse;
    [SerializeField] private Transform[] scarecrowTP;
    [SerializeField] private Transform sunPoint;
    [SerializeField] private Transform spiderPoint;

    [Header("Materials")]
    public Material whiteMaterial;
    private Material originalMaterial;

    public delegate void nightmareTime();
    public event nightmareTime nightmareMode;

    public delegate void endNightmareTime();
    public event endNightmareTime endNightmare;

    public delegate void endTutorial();
    public event endTutorial onEndTutorial;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalMaterial = sr.material;

        beginFight = false;
    }

    private void Update()
    {
        // boss starts in passive state
        if(beginFight == false && BossLocator.Instance.bossHealth.healthRatio < .9f && BossLocator.Instance.bossHealth.healthRatio != 0f)
        {
            onEndTutorial();
            beginFight = true;
            anim.SetTrigger("idle");

            miniAngel.SetActive(true);
        }

        // boss becomes aggressive
        if (beginFight == true)
        {
            // select state based on distance from player
            if (Vector2.Distance(transform.position, player.transform.position) <= 10f)
            {
                bossDistance = BossDistanceState.Close;
            }
            else if (Vector2.Distance(transform.position, player.transform.position) > 10f && Vector2.Distance(transform.position, player.transform.position) < 15f)
            {
                bossDistance = BossDistanceState.Mid;
            }
            else
            {
                bossDistance = BossDistanceState.Far;
            }

            // return to idle state
            if (currentlyInAction == false)
            {
                ResetToIdle();
                currentlyInAction = true;
            }

            // turn off/on mini angel
            if (activeSun == true)
            {
                miniAngel.SetActive(false);
            }
            else
            {
                miniAngel.SetActive(true);
            }
        }
    }

    private void FixedUpdate()
    {
        if (chase == true)
        {
            float distance = Mathf.Abs(transform.position.x - player.transform.position.x);
            if (distance > 4f)
            {
                Vector2 targetPos = new Vector2(player.transform.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position,targetPos, speed * Time.deltaTime);
                FaceTarget();
            }
            if (distance <= 4f)
            {
                chase = false;
                anim.SetBool("chase", false);
            }
        }
    }

    // resets boss back to idle state
    private void ResetToIdle()
    {
        anim.SetTrigger("idle");
    }

    // selects behavior based on state
    private void RestartAttackPattern()
    {
        switch (bossDistance)
        {
            case BossDistanceState.Close:
                CloseBehavior();
                break;
            case BossDistanceState.Mid:
                MidRangeBehavior();
                break;
            case BossDistanceState.Far:
                RangedBehavior();
                break;
        }
    }

    private void CloseBehavior()
    {
        int actionChoice = Random.Range(0, 2);

        if (actionChoice == 0)
        {
            FastCharge();
        }
        else if (actionChoice == 1)
        {
            MediumCharge();
        }
    }

    private void MidRangeBehavior()
    {
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

    private void RangedBehavior()
    {
        int actionChoice = Random.Range(0, 2);

        if (actionChoice == 0)
        {
            FastCharge();
        }
        else if (actionChoice == 1)
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
        postSlowChargeAttack();
        
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
            anim.SetTrigger("select");
        }
        else
        {
            anim.SetTrigger("jump");
        }

    }

    private void postMediumChargeAttack()
    {
        Debug.Log("medium attack!");

        int actionChoice = Random.Range(0, 2);
        if (actionChoice == 0)
        {
            anim.SetTrigger("meleeA");
        }
        else if (actionChoice == 1)
        {
            anim.SetTrigger("meleeC");
        }
    }

    private void postSlowChargeAttack()
    {
        Debug.Log("slow attack!");

        int actionChoice = Random.Range(0, 2);
        if (actionChoice == 0)
        {
            anim.SetBool("chase", true);
            chase = true;
        }
        else if (actionChoice == 1)
        {
            anim.SetTrigger("splash");
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
                flameAttack();
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
    private void flameAttack()
    {
        if(activeSun == false)
        {
            Instantiate(sunSpirit, sunPoint.position, Quaternion.identity);
            activeSun = true;
        }
    }

    // lightning attack
    private IEnumerator lightningAttack()
    {
        for (int i = 0; i < 9; i += 3)
        {
            Instantiate(lightningBolt, new Vector2(player.transform.position.x, -5), Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }

    //anvil attack
    private void anvilAttack()
    {
        Instantiate(anvil, new Vector2(player.transform.position.x, 15), Quaternion.identity);
    }

    // scarecrow attack
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
    // creates scarecrow attack explosion
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

    // teleport to the ceiling and transform
    private void SpiderTeleportAttack()
    {
        anim.SetTrigger("spider");
        transform.position = spiderPoint.position;
        rb.gravityScale = 0f;
        rb.velocity = Vector3.zero;
    }

    // enables selection of new passive action
    public void EndCurrentAttack()
    {
        Debug.Log("attack ended");
        currentlyInAction = false;
    }

    // enters nightmare time
    public void enterNightmare()
    {
        anim.SetTrigger("nightmareTime");
        nightmareMode();
    }

    // exit nightmare time
    public void exitNightmare()
    {
        endNightmare();
    }

    // first nightmare mode attack
    public void summonTentacleSigns()
    {
        for (int i = 5; i < 30; i += 5)
        {
            Instantiate(tentacleSign, new Vector2(transform.position.x + i, transform.position.y), Quaternion.identity);
            Instantiate(tentacleSign, new Vector2(transform.position.x - i, transform.position.y), Quaternion.identity);
        }
    }

    public void secondNightmareAttack()
    {
        anim.SetTrigger("witchFollowUp");
        transform.position = new Vector2(5f, -2.5f);
        rb.gravityScale = 1f;
    }

    // second nightmare attack
    public void summonTentacleSpears()
    {
        for (int i = 4; i < 24; i += 4)
        {
            Instantiate(tentacleSpear, new Vector2(transform.position.x + i, transform.position.y), Quaternion.identity);
            Instantiate(tentacleSpear, new Vector2(transform.position.x - i, transform.position.y), Quaternion.identity);
        }
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

    // dash forward
    public void dashForward()
    {
        rb.AddForce(transform.right * 30f * transform.localScale.x, ForceMode2D.Impulse);
    }

    // stop moving
    public void stopMovement()
    {
        rb.velocity = Vector2.zero;
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

    // creates ripple effect on summoning sign pole
    private void createRipples()
    {
        Instantiate(ripples, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }

    // creates blast effect on melee attack
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

    // jumps backward
    private void jumpBack()
    {
        float backwardDirection = transform.localScale.x > 0 ? -1f : 1f;

        Vector2 jumpVector = new Vector2(backwardDirection * 10, 7);
        rb.AddForce(jumpVector, ForceMode2D.Impulse);
    }

    // change sprite to all white when hit
    public void FlashWhite()
    {
        sr.material = whiteMaterial;
        Invoke("ResetMaterial", 0.1f);
    }

    // reset sprite to normal material
    private void ResetMaterial()
    {
        sr.material = originalMaterial;
    }
}
