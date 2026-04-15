using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    public DialogueObject dialogueObject;
    public DialogueObject dialogueCorrect;
    public DialogueObject dialogueWrong;
    public DialogueObject dialogueShot;
    public DialogueObject dialogueSpicy;

    public void Interact(DialogueObject dialogueChoice)
    {
        Locator.Instance.responseManager.DestroyResponses();

        if (TryGetComponent(out DialogueResponseEvent responseEvents) && responseEvents.DialogueObject == dialogueObject)
        {
            Locator.Instance.dialogueUI.AddResponseEvents(responseEvents.Events);
        }

        Locator.Instance.dialogueUI.ShowDialogue(dialogueChoice);
    }
}
