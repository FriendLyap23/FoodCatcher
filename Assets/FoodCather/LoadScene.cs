using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadNextScene(int indexScene) 
    {
        SceneManager.LoadScene(indexScene);
    }
}
