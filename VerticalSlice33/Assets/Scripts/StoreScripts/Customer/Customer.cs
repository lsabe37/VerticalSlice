using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] DialogueActivator dialogueActivator;
    public SpriteRenderer spriteRenderer;

    [Header("Sprites")]
    [SerializeField] Sprite Neutral;
    [SerializeField] Sprite Correct;
    [SerializeField] Sprite Wrong;
    [SerializeField] Sprite LightsOff;
    [SerializeField] Sprite Shot;
    [SerializeField] Sprite Spicy;

    [Header("Other")]
    public Vector2 targetPosition;
    public float duration = .2f;
    public Sprite ID;
    [HideInInspector] public bool servingCustomer;
    public int desiredDonut;
    public bool imposter;


    private void OnEnable()
    {
        StartCoroutine(MoveToTarget(targetPosition, duration));
        spriteRenderer.sprite = Neutral;
    }

    IEnumerator MoveToTarget(Vector2 target, float time)
    {
        Vector2 startPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;
            transform.position = Vector3.Lerp(startPosition, target, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target;

        TalkToCustomer();
    }


    private void TalkToCustomer()
    {
        servingCustomer = true;
        dialogueActivator.Interact(dialogueActivator.dialogueObject);
    }

    public void CorrectReaction()
    {
        dialogueActivator.Interact(dialogueActivator.dialogueCorrect);
        spriteRenderer.sprite = Correct;
    }

    public void WrongReaction()
    {
        dialogueActivator.Interact(dialogueActivator.dialogueWrong);
        spriteRenderer.sprite = Wrong;
    }

    public void shotReaction()
    {
        dialogueActivator.Interact(dialogueActivator.dialogueShot);
        spriteRenderer.sprite = Shot;
    }

    public void spicyReaction()
    {
        dialogueActivator.Interact(dialogueActivator.dialogueSpicy);
        spriteRenderer.sprite = Spicy;
    }

    public void NeutralSprite()
    {
        spriteRenderer.sprite = Neutral;
    }
    public void CorrectSprite()
    {
        spriteRenderer.sprite = Correct;
    }
    public void WrongSprite()
    {
        spriteRenderer.sprite = Wrong;
    }
    public void SpiceSprite()
    {
        spriteRenderer.sprite = Spicy;
    }

    public void Hop()
    {
        StartCoroutine(PerformHop());
    }
    private IEnumerator PerformHop()
    {
        float jumpDuration = .2f;
        Vector3 startPos = transform.position;
        Vector3 peakPos = startPos + Vector3.up * .5f;

        float elapsedTime = 0;
        while (elapsedTime < jumpDuration)
        {
            transform.position = Vector3.Lerp(startPos, peakPos, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = peakPos;

        elapsedTime = 0;
        while (elapsedTime < jumpDuration)
        {
            transform.position = Vector3.Lerp(peakPos, startPos, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = startPos;
    }
}

