using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayeStatusUI : MonoBehaviour
{
    [SerializeField] private Image totalHP;
    [SerializeField] private Image currentHP;

    [SerializeField] private Image totalEnergy;
    [SerializeField] private Image currentEnergy;

    private void Start()
    {
        currentHP.fillAmount = 1f;
        currentEnergy.fillAmount = 1f;

    }

    private void Update()
    {
        currentHP.fillAmount = PlayerLocator.Instance.playerHealth.health / PlayerLocator.Instance.playerHealth.maxHealth;
        currentEnergy.fillAmount = PlayerLocator.Instance.playerEnergy.energy / PlayerLocator.Instance.playerEnergy.maxEnergy;
    }
}
