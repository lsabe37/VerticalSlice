using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    private void Start()
    {
        Locator.Instance.gameManager.lightsTurnOff += turnOffLights;
    }

    private void turnOffLights()
    {
        if (Locator.Instance.gameManager.lightsOff == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
