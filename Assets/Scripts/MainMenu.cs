using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.sceneLoaded += CreateGameManager;
        SceneManager.LoadScene($"Level1");
    }

    private void CreateGameManager(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= CreateGameManager;
        GameManager existGameManager = FindAnyObjectByType<GameManager>();
        if(existGameManager == null)
        {
            GameObject gameManager = new GameObject("GameManager");
            gameManager.AddComponent<GameManager>(); 
        }
    }
}
