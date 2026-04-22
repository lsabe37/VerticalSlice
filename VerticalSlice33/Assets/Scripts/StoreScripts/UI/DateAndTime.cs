using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateAndTime : MonoBehaviour
{
    [SerializeField] private Image Time;
    [SerializeField] private Sprite[] Times;
    [SerializeField] private Image[] Dates;

    private void Start()
    {
        Locator.Instance.customerManager.left += changeTime;
    }

    private void changeTime()
    {
        if (Locator.Instance.customerManager.customerServed < 2)
        {
            Time.sprite = Times[0];
        }

        else if (Locator.Instance.customerManager.customerServed >= 2 && Locator.Instance.customerManager.customerServed < 4)
        {
            Time.sprite = Times[1];
        }

        else if (Locator.Instance.customerManager.customerServed >= 4)
        {
            Time.sprite = Times[2];
        }
    }

    private void changeDate()
    {
        //if(gameManager.day == 1)
        //elseif(gameManager.day == 2)
        //elseif(gameManager.day == 3)
    }
}
