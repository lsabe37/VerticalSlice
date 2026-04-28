using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public enum StoreStateDays { Day1, Day2, Day3 }
    public StoreStateDays storeDay;

    public enum StoreStateTime { Morning, Noon, Evening }
    public StoreStateTime storeTime;

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
            storeTime = StoreStateTime.Morning;
            Time.sprite = Times[0];
        }

        else if (Locator.Instance.customerManager.customerServed >= 2 && Locator.Instance.customerManager.customerServed < 4)
        {
            storeTime = StoreStateTime.Noon;
            Time.sprite = Times[1];
        }

        else if (Locator.Instance.customerManager.customerServed >= 4)
        {
            storeTime = StoreStateTime.Evening;
            Time.sprite = Times[2];
        }
    }
}
