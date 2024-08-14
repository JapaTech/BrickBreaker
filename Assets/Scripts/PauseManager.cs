using UnityEngine;

public class PauseManager : MonoBehaviour
{
    //Criação do singleton
    public static PauseManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    //Variável que fala se o jogo está pausada ou não
    public bool Paused { get; private set; }

    //Pausa o jogo deixando o tempo da física em 0
    public void Pause()
    {
        Paused = true;
        Time.timeScale = 0;
    }

    //Tira o jogo do pause e retorna o tempo da física dele para 1
    public void Unpause()
    {
        Paused = false;
        Time.timeScale = 1;
    }
}
