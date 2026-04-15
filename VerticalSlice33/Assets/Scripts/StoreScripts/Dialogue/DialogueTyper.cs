using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTyper : MonoBehaviour
{
    [SerializeField] private float typingSpeed;

    public Coroutine RunTyper(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(routine: TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;

        float timer = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            timer += Time.deltaTime * typingSpeed;
            charIndex = Mathf.FloorToInt(timer);
            charIndex = Mathf.Clamp(value: charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring(0, charIndex);

            yield return null;
        }

        textLabel.text = textToType;
    }
}
