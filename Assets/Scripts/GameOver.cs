using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    //Texto do game over
    [SerializeField] private TMP_Text gameOVerMessage;
    //Texto que mostra o jogador o caminho do arquivo de salavamento
    [SerializeField] private TMP_Text saveScoreMessage;

    //Menagem de quando o jogador passar todas as fases
    [TextArea]
    [SerializeField] string winMessage;

    //Menagem de quando o jogador perdeu
    [TextArea]
    [SerializeField] string tryAginMessage;

    private void Start()
    {
        ShowMessage();
        saveScoreMessage.text = "Seu progresso foi salvo no " + GameManager.Instance.WriteScore();
    }

    //Troca a mensagem de final do jogo a depender se o player ganhou ou não
    public void ShowMessage()
    {
        if (GameManager.Instance.WinGame == true)
        {
            gameOVerMessage.text = winMessage + $" Sua pontuação final foi de: {GameManager.Instance.Score}";
        }
        else
        {
            gameOVerMessage.text = tryAginMessage + $" Sua pontuação final foi de: {GameManager.Instance.Score}";
        }
    }

    //Volta para o menu inicial
    public void MainMenu()
    {
        GameManager.Instance.MainMenu();
    }
}
