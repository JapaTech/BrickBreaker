using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image lives;
    [SerializeField] private TMP_Text score;

    public void UpdateHealth(int current, int max)
    {
        lives.rectTransform.sizeDelta = new Vector2(lives.rectTransform.sizeDelta.x * current / max, lives.rectTransform.sizeDelta.y);
    }

    public void UpdateScore(int value)
    {
        score.text = "Pontos: " + value.ToString();
    }
}
