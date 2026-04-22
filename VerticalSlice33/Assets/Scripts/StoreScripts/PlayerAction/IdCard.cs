using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IdCard : MonoBehaviour
{
    public Image uiImage;

    public CustomerManager customerManager;

    private void Update()
    {
        updateSprite();
    }

    private void updateSprite()
    {
        if (uiImage != null && customerManager.currentCharID != null)
        {
            uiImage.sprite = customerManager.currentCharID;
        }
    }
}
