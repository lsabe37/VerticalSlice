using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource bgmSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip bgm;
    [SerializeField] private AudioClip customerEnterSFX;
    [SerializeField] private AudioClip buttonSFX;
    [SerializeField] private AudioClip reloadSFX;
    [SerializeField] private AudioClip closeDialogueSFX;
    [SerializeField] private AudioClip gunShotSFX;
    [SerializeField] private AudioClip wrongSFX;
    [SerializeField] private AudioClip spiceSFX;
    [SerializeField] private AudioClip[] dialogueSFX;

    [Header("Pitch Variation")]
    [SerializeField][Range(0f, 0.1f)] private float pitchRange = 0.05f;
    private float basePitch = 1f;

    public int bleepInterval = 4;

    private void Start()
    {
        Locator.Instance.customerManager.enter += EnterSFX;
        Locator.Instance.customerManager.wrong += WrongSFX;
        Locator.Instance.customerManager.spiceTest += SpiceSFX;
        Locator.Instance.gameManager.shootGun += GunShot;

        bgmSource.PlayOneShot(bgm, 1.0f);
    }

    private void EnterSFX()
    {
        audioSource.PlayOneShot(customerEnterSFX, 1.0f);
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

    private void WrongSFX()
    {
        audioSource.PlayOneShot(wrongSFX, 1.0f);
    }

    private void SpiceSFX()
    {
        audioSource.PlayOneShot(spiceSFX, 1.0f);
    }

    public void DialogueSFX()
    {
        int randomIndex = Random.Range(0, dialogueSFX.Length);
        AudioClip clipToPlay = dialogueSFX[randomIndex];

        audioSource.pitch = basePitch + Random.Range(-pitchRange, pitchRange);

        audioSource.PlayOneShot(clipToPlay, 0.5f);
    }
}
