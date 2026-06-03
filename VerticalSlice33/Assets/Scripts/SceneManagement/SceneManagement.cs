using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void LoadIntroScene()
    {
        SceneManager.LoadScene("Intro");
    }

    public void LoadStoreScene()
    {
        SceneManager.LoadScene("Store");
    }

    public void LoadBattleScene()
    {
        SceneManager.LoadScene("Battle");
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
