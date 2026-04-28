using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePlayer : MonoBehaviour
{
    public float PlayerHP;
    public float currentHP;

    private void Start()
    {
        currentHP = PlayerHP;
        Locator.Instance.customerManager.served += updateSanity;
    }

    public void updateSanity()
    {
        if (Locator.Instance.customerManager.correctOrder == true)
        {
            if (currentHP < PlayerHP)
            {
                currentHP += 10f;
            }
            else
            {
                currentHP = PlayerHP;
            }
        }
        else
        {
            currentHP -= 10f;
        }
    }

    public void resetHP()
    {
        currentHP = PlayerHP;
    }

}
