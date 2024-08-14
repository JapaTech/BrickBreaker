using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Sprite para cada quantidade de vidas
    [SerializeField] private Sprite[] lives;
    //Referência da imagem de vida
    [SerializeField] private Image live;
    //Texto que exibe a pontuação do jogador
    [SerializeField] private TMP_Text score;
    //Painel que mostra os botões para ir para o próximo nível
    [field: SerializeField] public GameObject NextLevelPannel { get; private set; }

    //Singleton
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

    //Atualiza os sprites da vida
    public void UpdateHealth(int current)
    {
        if (current <= 0)
            return;
        live.sprite = lives[current - 1];
    }

    //Atualiza o texto da pontuação
    public void UpdateScore(int value)
    {
        score.text = "Pontos: " + value.ToString();
    }

    //Avança para a próxima fase
    public void NextLevel_Btn()
    {
        GameManager.Instance.LoadLevel(GameManager.Instance.Level + 1);
    }

}
