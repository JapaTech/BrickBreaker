using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image lives;
    [SerializeField] private TMP_Text score;
    [field: SerializeField] public GameObject nextLevelPannel { get; private set; }

    public void UpdateHealth(int current, int max)
    {
        float newX = lives.rectTransform.parent.GetComponent<RectTransform>().sizeDelta.x * (float) current / max;
        lives.rectTransform.sizeDelta = new Vector2(newX, lives.rectTransform.sizeDelta.y);
    }

    public void UpdateScore(int value)
    {
        score.text = "Pontos: " + value.ToString();
    }

    public void NextLevel_Btn()
    {
        GameManager.Instance.LoadLevel(GameManager.Instance.Level);
    }

}
