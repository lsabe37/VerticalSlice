using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public float energy = 10f;
    public float maxEnergy = 10f;

    private void Start()
    {
        PlayerLocator.Instance.player.tryParry += useEnergy;
    }

    private void FixedUpdate()
    {
        if (energy < maxEnergy)
        {
            energy += 0.1f;
        }
    }

    private void useEnergy()
    {
        energy -= 5f;
    }
}
