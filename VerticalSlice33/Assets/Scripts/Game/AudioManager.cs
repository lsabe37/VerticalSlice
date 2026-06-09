using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip customerTalkSFX;
    [SerializeField] private AudioClip buttonSFX;
    [SerializeField] private AudioClip reloadSFX;
    [SerializeField] private AudioClip closeDialogueSFX;
    [SerializeField] private AudioClip gunShotSFX;

    private void Start()
    {
        Locator.Instance.customerManager.OnInteract += CustomerTalk;
        Locator.Instance.gameManager.shootGun += GunShot;

    }

    private void CustomerTalk()
    {
        audioSource.PlayOneShot(customerTalkSFX, 1.0f);
    }

    public void ButtonSFX()
    {
        audioSource.PlayOneShot(buttonSFX, 1.0f);
    }

    public void GunSFX()
    {
        audioSource.PlayOneShot(reloadSFX, 1.0f);
    }

    public void CloseSFX()
    {
        audioSource.PlayOneShot(closeDialogueSFX, 1.0f);
    }

    private void GunShot()
    {
        audioSource.PlayOneShot(gunShotSFX, 1.0f);
    }
}
