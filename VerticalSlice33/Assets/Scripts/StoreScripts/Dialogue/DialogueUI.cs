using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject speechDialogue;
    [SerializeField] private GameObject dialogueBox;

    private DialogueTyper typer;

    public bool isTalking;


    private void Start()
    {
        typer = GetComponent<DialogueTyper>();
        CloseDialogueBox();
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        Locator.Instance.responseManager.AddResponseEvents(responseEvents);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        isTalking = true;
        dialogueBox.SetActive(true);
        textLabel.text = string.Empty;
        StartCoroutine(routine: StepThroughDialogue(dialogueObject));
    }


    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return typer.RunTyper(dialogue, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }

        if (dialogueObject.HasResponses)
        {
            Locator.Instance.responseManager.ShowResponses(dialogueObject.Responses);
        }

        else
        {
            CloseDialogueBox();
            isTalking = false;
        }
    }


    public void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
