using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donuts : MonoBehaviour
{
    public int donutID;

    public GameManager gameManager;
    public CustomerManager customerManager;


    private void OnMouseDown()
    {
        gameManager.SelectedDonutID = donutID;
        gameManager.SwitchToCustomers();
        customerManager.customerReact();
    }
}
