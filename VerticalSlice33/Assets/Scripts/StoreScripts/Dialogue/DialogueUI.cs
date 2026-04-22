using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject speechDialogue;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Button closeBoxButton;

    private DialogueTyper typer;

    public bool isTalking;
    public bool interruptDialogue;
    private Coroutine activeDialogueRoutine;


    private void Start()
    {
        typer = GetComponent<DialogueTyper>();
        CloseDialogueBox();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && closeBoxButton.gameObject.activeSelf)
        {
            closeBoxButton.onClick.Invoke();
        }
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
        if(interruptDialogue == true)
        {
            StopCoroutine(activeDialogueRoutine);
            interruptDialogue = false;
        }
        activeDialogueRoutine = StartCoroutine(routine: StepThroughDialogue(dialogueObject));
        closeBoxButton.gameObject.SetActive(false);
    }


    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return typer.RunTyper(dialogue, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses)
            {
                closeBoxButton.gameObject.SetActive(true);
                break;
            }

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
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
        closeBoxButton.gameObject.SetActive(false);
    }

}
