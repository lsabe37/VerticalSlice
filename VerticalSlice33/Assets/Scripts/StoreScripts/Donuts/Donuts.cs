using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donuts : MonoBehaviour
{
    public int donutID;

    public GameManager gameManager;
    public CustomerManager customerManager;

    public float newValue = 0.3f;
    private Material donutMaterial;

    private void Start()
    {
        donutMaterial = GetComponent<SpriteRenderer>().material;
    }

    private void OnMouseDown()
    {
        gameManager.SelectedDonutID = donutID;
        gameManager.SwitchToCustomers();
        customerManager.customerReact();
    }

    private void OnMouseEnter()
    {
        donutMaterial.SetFloat("_OutlineThickness", newValue);
    }

    private void OnMouseExit()
    {
        donutMaterial.SetFloat("_OutlineThickness", 0f);
    }
}
