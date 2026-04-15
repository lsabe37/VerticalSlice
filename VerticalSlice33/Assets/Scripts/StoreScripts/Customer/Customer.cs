using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Customers : MonoBehaviour
{
    [Header("Components")]
    public SpriteRenderer spriteRenderer;

    [Header("Sprites")]
    [SerializeField] Sprite Neutral;
    [SerializeField] Sprite Happy;
    [SerializeField] Sprite Upset;
    [SerializeField] Sprite LightsOff;
    [SerializeField] Sprite Shot;
    [SerializeField] Sprite Spicy;

    [Header("Other")]
    public Vector2 targetPosition;
    public float duration = .2f;
    public Sprite ID;
    public bool servingCustomer;
    public int desiredDonut;


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
        //dialogue
    }



}
