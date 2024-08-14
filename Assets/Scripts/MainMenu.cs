using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Referência para o painel tutorial
    [SerializeField] private GameObject tutorial;

    //Referência para o painel para o jogador confirmar se quer sair
    [SerializeField] private GameObject exitGame;

    private void Start()
    {
        //Desatvia os paineis no começo do jogo
        Tutorial(false);
        ExitGame(false);
    }

    //Vai para a fase 1
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        if(PauseManager.Instance != null)
        {
            PauseManager.Instance.Unpause();
        }
    }

    //Ativa ou desativa o painel de tutorial
    public void Tutorial(bool active)
    {
        tutorial.SetActive(active);
    }

    //Ativa ou desativa o painel para sair do jogo
    public void ExitGame(bool active)
    {
        exitGame.SetActive(active);
    }

    //Fecha o jogo
    public void EndApp()
    {
        Application.Quit();
    }
}
