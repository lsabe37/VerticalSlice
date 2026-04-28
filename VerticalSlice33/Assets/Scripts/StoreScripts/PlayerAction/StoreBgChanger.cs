using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBgChanger : MonoBehaviour
{
    public GameObject storeStitches;
    public GameObject storeEyes;

    public void revealHidden()
    {
        storeEyes.SetActive(true);
        storeStitches.SetActive(true);
    }

    public void hideHidden()
    {
        storeEyes.SetActive(false);
        storeStitches.SetActive(false);
    }
}
