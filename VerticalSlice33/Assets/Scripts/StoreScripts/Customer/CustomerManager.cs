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
    [HideInInspector] public int customerServed = 0;
    private int customerNumber = -1;
    private bool greetCustomer;
    public bool customerPresent;
    private int randomCustomer;
    public int requiredDonutID;
    [HideInInspector] public bool donutServed;
    [HideInInspector] public bool correctOrder;
    private float timer;
    public Sprite currentCharID;
    public Transform spawnLocation;
    [SerializeField] private Vector2 exitLocation;
    private bool customerIsLeaving;

    private bool customerIsFake;

    [Header("References")]
    public SceneManagement sceneManager;

    public delegate void customerServedEvent();
    public event customerServedEvent served;

    public delegate void customerAppearEvent();
    public event customerAppearEvent appeared;

    public delegate void customerLeaveEvent();
    public event customerLeaveEvent left;

    public delegate void wrongOrderEvent();
    public event wrongOrderEvent wrong;

    public delegate void spicyEvent();
    public event spicyEvent spiceTest;

    public delegate void talkEvent();
    public event talkEvent OnInteract;

    [Header("Other")]
    public GameObject correctText;
    public GameObject wrongText;

    private void Update()
    {
        if (greetCustomer == true && customerServed < totalNumberOfCustomers)
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
            if(customerIsLeaving == true)
            {
                StartCoroutine(customerLeave());
            }
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

        customerIsFake = Customers.imposter;

        if(correctText.activeInHierarchy == true || wrongText.activeInHierarchy == true)
        {
            correctText.SetActive(false);
            wrongText.SetActive(false);
        }
    }

    public IEnumerator customerLeave()
    {
        customerIsLeaving = false;

        if(Locator.Instance.gameManager.wasShot == true && customerIsFake == true)
        {
            sceneManager.LoadBattleScene();
        }

        Vector2 startPosition = transform.position;
        float elapsedTime = 0;
        float time = .3f;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;

            currentCustomer.gameObject.transform.position = Vector3.Lerp(startPosition, exitLocation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentCustomer.gameObject.transform.position = exitLocation;

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
            correctText.SetActive(true);
        }
        else
        {
            Customers.WrongReaction();
            correctOrder = false;
            wrong();
            wrongText.SetActive(true);
        }

        Locator.Instance.gameManager.DisableNavigationUI();
        served();

        customerIsLeaving = true;
    }

    public void customerShotReact()
    {
        Locator.Instance.gameManager.DisableNavigationUI();
        Locator.Instance.gameManager.changeBg();
        Customer Customers = currentCustomer.GetComponent<Customer>();
        Customers.shotReaction();

        if(Customers.imposter == true)
        {
            correctOrder = true;

        }
        else
        {
            correctOrder = false;
        }
        served();

        customerIsLeaving = true;
    }

    public void customerSpiceReaction()
    {
        Customer Customers = currentCustomer.GetComponent<Customer>();
        Customers.spicyReaction();
        spiceTest();
    }

    public void CustomerHop()
    {
        Customer Customers = currentCustomer.GetComponent<Customer>();
        Customers.Hop();
        OnInteract();
    }
}
