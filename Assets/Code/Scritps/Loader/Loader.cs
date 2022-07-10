using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public void Load(int indexScene)
    {
        SceneManager.LoadSceneAsync(indexScene);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
