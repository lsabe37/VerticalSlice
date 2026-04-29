using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void LoadStoreScene()
    {
        SceneManager.LoadScene("Store");
    }

    public void LoadBattleScene()
    {
        SceneManager.LoadScene("Battle");
    }
}
