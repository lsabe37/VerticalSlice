using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public void showPauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void hidePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }
}
