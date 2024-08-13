using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text endMessage;
    [SerializeField] private TMP_Text saveMessage;

    [TextArea]
    [SerializeField] string winMessage;

    [TextArea]
    [SerializeField] string tryAginMessage;

    private void Start()
    {
        ShowMessage();
        saveMessage.text = "Seu progresso foi salvo no " + GameManager.Instance.WriteScore();
    }

    public void ShowMessage()
    {
        if (GameManager.Instance.WinGame == true)
        {
            endMessage.text = winMessage + $" Sua pontuação final foi de: {GameManager.Instance.Score}";
        }
        else
        {
            endMessage.text = tryAginMessage + $" Sua pontuação final foi de: {GameManager.Instance.Score}";
        }
    }

    public void MainMenu()
    {
        GameManager.Instance.MainMenu();
    }
}
