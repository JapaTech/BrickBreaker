using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Sprite[] lives;
    [SerializeField] private Image live;
    [SerializeField] private TMP_Text score;
    [field: SerializeField] public GameObject nextLevelPannel { get; private set; }

    public static UIManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateHealth(int current)
    {
        if (current <= 0)
            return;
        live.sprite = lives[current - 1];
    }

    public void UpdateScore(int value)
    {
        score.text = "Pontos: " + value.ToString();
    }

    public void NextLevel_Btn()
    {
        GameManager.Instance.LoadLevel(GameManager.Instance.Level + 1);
        
    }

}
