using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    

    public void ShowMessage()
    {
        if (GameManager.Instance.Level == 3 )
        {
            text.text = "";
        }
        else
        {
            text.text = "";
        }
    }

    public void SaveScore()
    {
        GameManager.Instance.WriteScore();
    }

    public void MainMenu()
    {

        GameManager.Instance.MainMenu();
    }
}
