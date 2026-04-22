using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [Header("Customers")]
    public GameObject[] customers;
    public GameObject[] Day1Customers;
    public GameObject[] Day2Customers;
    public GameObject[] Day3Customers;
    private GameObject currentCustomer;

    [Header("Customer Context")]
    public int totalNumberOfCustomers = 5;
    public int customerServed = 0;
    public int customersRemaining = 4;
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

    public delegate void customerServedEvent();
    public event customerServedEvent served;

    public delegate void customerLeaveEvent();
    public event customerLeaveEvent left;

    public delegate void wrongOrderEvent();
    public event wrongOrderEvent wrong;

    public delegate void spicyEvent();
    public event spicyEvent spiceTest;

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

        if (Locator.Instance.dialogueUI.isTalking == false && customerPresent == true && (donutServed == true || Locator.Instance.gameManager.wasShot == true))
        {
            customerLeave();
            Locator.Instance.gameManager.resetBg();
            Debug.Log("customer has departed");
            Locator.Instance.gameManager.DisableActionUI();
        }
    }

    public void SelectCustomer()
    {
        switch (Locator.Instance.storeManager.storeDay)
        {
            case StoreManager.StoreStateDays.Day1:
                customers = Day1Customers;
                break;
            case StoreManager.StoreStateDays.Day2:
                customers = Day2Customers;
                break;
            case StoreManager.StoreStateDays.Day3:
                customers = Day3Customers;
                break;
        }

        currentCustomer = Instantiate(customers[customerNumber], spawnLocation.position, spawnLocation.rotation);
        Customer Customers = customers[customerNumber].GetComponent<Customer>();
        currentCharID = Customers.ID;

        Locator.Instance.gameManager.EnableActionUI();
        Locator.Instance.gameManager.EnableNavigationUI();
    }

    public void customerLeave()
    {
        Destroy(currentCustomer.gameObject);
        customerPresent = false;
        donutServed = false;
        Locator.Instance.gameManager.wasShot = false;
        customerServed += 1;
        left();
    }

    public void customerReact()
    {
        donutServed = true;
        Customer Customers = currentCustomer.GetComponent<Customer>();

        if (Customers.desiredDonut == Locator.Instance.gameManager.SelectedDonutID)
        {
            Customers.CorrectReaction();
            correctOrder = true;
        }
        else
        {
            Customers.WrongReaction();
            correctOrder = false;
            wrong();
        }

        served();
    }

    public void customerShotReact()
    {
        Locator.Instance.dialogueUI.interruptDialogue = true;
        Locator.Instance.gameManager.DisableNavigationUI();
        Locator.Instance.gameManager.changeBg();
        Customer Customers = currentCustomer.GetComponent<Customer>();
        Customers.shotReaction();
    }

    public void customerSpiceReaction()
    {
        Locator.Instance.dialogueUI.interruptDialogue = true;
        Customer Customers = currentCustomer.GetComponent<Customer>();
        Customers.spicyReaction();
        spiceTest();
    }
}
