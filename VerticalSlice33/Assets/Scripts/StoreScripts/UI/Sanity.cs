using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Sanity : MonoBehaviour
{
    [SerializeField] private StorePlayer player;
    [SerializeField] private Image sanityTotal;
    [SerializeField] private Image sanityCurrent;

    private void Start()
    {
        sanityTotal.fillAmount = 1f;

    }

    private void Update()
    {
        sanityCurrent.fillAmount = player.currentHP / player.PlayerHP;
    }
}