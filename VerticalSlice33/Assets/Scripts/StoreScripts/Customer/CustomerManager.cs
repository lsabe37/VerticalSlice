using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customerManager : MonoBehaviour
{
    [Header("Customers")]
    public GameObject[] customers;
    public GameObject[] Day1Customers;
    private GameObject currentCustomer;

    [Header("Customer Context")]
    public int totalNumberOfCustomers = 5;
    public int customerServed = 0;
    private int customerNumber = -1;

    private bool greetCustomer;
    public bool customerPresent;
    private int randomCustomer;
    public int requiredDonutID;
    private bool donutServed;
    public bool correctOrder;
    private float timer;
    public Sprite currentCharID;
    public Transform spawnLocation;



    private void Update()
    {
        if (greetCustomer == true)
        {
            customerPresent = true;
            greetCustomer = false;

            if (customerServed < totalNumberOfCustomers)
            {
                customerNumber += 1;
            }
            else
            {
                customerNumber = -1;
            }

            SelectCustomer();
        }

        if (customerPresent != true)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                greetCustomer = true;
                timer = 0;
            }
        }

    }

    public void SelectCustomer()
    {
        
    }

    public void customerLeave()
    {
       
    }

    public void customerReact()
    {

    }

}
