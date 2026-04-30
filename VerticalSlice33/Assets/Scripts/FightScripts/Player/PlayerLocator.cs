using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
    public static PlayerLocator Instance { get; private set; }
    public PlayerHealth playerHealth { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        GameObject playerObject = GameObject.FindWithTag("Player");
        playerHealth = playerObject.GetComponent<PlayerHealth>();
    }
}
