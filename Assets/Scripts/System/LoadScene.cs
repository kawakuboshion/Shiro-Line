using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void OnLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReStartCurrentScene()
    {
        string current = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(current);
    }
}
