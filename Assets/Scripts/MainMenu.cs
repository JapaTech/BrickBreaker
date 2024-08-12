using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject exitGame;

    private void Start()
    {
        Tutorial(false);
        ExitGame(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Tutorial(bool active)
    {
        tutorial.SetActive(active);
    }

    public void ExitGame(bool active)
    {
        exitGame.SetActive(active);
    }

    public void EndApp()
    {
        Application.Quit();
    }
}
