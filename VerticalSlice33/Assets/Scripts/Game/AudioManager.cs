using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip customerTalkSFX;

    private void Start()
    {
        Locator.Instance.customerManager.OnInteract += CustomerTalk;
    }

    private void CustomerTalk()
    {
        audioSource.PlayOneShot(customerTalkSFX, 1.0f);
    }
}
