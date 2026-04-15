using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locator : MonoBehaviour
{
    public static Locator Instance { get; private set; }
    public CustomerManager customerManager { get; private set; }
    public ResponseManager responseManager { get; private set; }
    public GameManager gameManager { get; private set; }

    public DialogueUI dialogueUI { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        GameObject gameManagerObject = GameObject.FindWithTag("gameManager");
        customerManager = gameManagerObject.GetComponent<CustomerManager>();
        responseManager = gameManagerObject.GetComponent<ResponseManager>();
        gameManager = gameManagerObject.GetComponent<GameManager>();

        GameObject uiGameObject = GameObject.FindWithTag("UI");
        dialogueUI = uiGameObject.GetComponent<DialogueUI>();
    }
}
