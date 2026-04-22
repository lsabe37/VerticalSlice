using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public enum StoreStateDays { Day1, Day2, Day3 }
    public StoreStateDays storeDay;

    public enum StoreStateTime { Morning, Noon, Evening }
    public StoreStateTime storeTime;

    private void Start()
    {
        Locator.Instance.customerManager.left += changeTime;
    }

    private void changeTime()
    {
        if (Locator.Instance.customerManager.customerServed < 2)
        {
            storeTime = StoreStateTime.Morning;
        }

        else if (Locator.Instance.customerManager.customerServed >= 2 && Locator.Instance.customerManager.customerServed < 4)
        {
            storeTime = StoreStateTime.Noon;
        }

        else if (Locator.Instance.customerManager.customerServed >= 4)
        {
            storeTime = StoreStateTime.Evening;
        }
    }
}
