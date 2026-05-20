using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitboxManager : MonoBehaviour
{
    public GameObject spinHitbox;
    public GameObject thrustHitbox;
    public GameObject melee1Hitbox;
    public GameObject melee2Hitbox;
    public GameObject melee3Hitbox;
    public GameObject melee4Hitbox;

    private void toggleSpinHitbox()
    {
        if (spinHitbox.activeInHierarchy)
        {
            spinHitbox.SetActive(false);
        }
        else
        {
            spinHitbox.SetActive(true);
        }
    }

    private void toggleThrustHitbox()
    {
        if (thrustHitbox.activeInHierarchy)
        {
            thrustHitbox.SetActive(false);
        }
        else
        {
            thrustHitbox.SetActive(true);
        }
    }

    private void toggleMelee1Hitbox()
    {
        if (melee1Hitbox.activeInHierarchy)
        {
            melee1Hitbox.SetActive(false);
        }
        else
        {
            melee1Hitbox.SetActive(true);
        }
    }

    private void toggleMelee2Hitbox()
    {
        if (melee2Hitbox.activeInHierarchy)
        {
            melee2Hitbox.SetActive(false);
        }
        else
        {
            melee2Hitbox.SetActive(true);
        }
    }

    private void toggleMelee3Hitbox()
    {
        if (melee3Hitbox.activeInHierarchy)
        {
            melee3Hitbox.SetActive(false);
        }
        else
        {
            melee3Hitbox.SetActive(true);
        }
    }

    private void toggleMelee4Hitbox()
    {
        if (melee4Hitbox.activeInHierarchy)
        {
            melee4Hitbox.SetActive(false);
        }
        else
        {
            melee4Hitbox.SetActive(true);
        }
    }
}
