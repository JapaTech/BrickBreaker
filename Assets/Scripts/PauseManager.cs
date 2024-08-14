using UnityEngine;

public class PauseManager : MonoBehaviour
{
    //Cria��o do singleton
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

    //Vari�vel que fala se o jogo est� pausada ou n�o
    public bool Paused { get; private set; }

    //Pausa o jogo deixando o tempo da f�sica em 0
    public void Pause()
    {
        Paused = true;
        Time.timeScale = 0;
    }

    //Tira o jogo do pause e retorna o tempo da f�sica dele para 1
    public void Unpause()
    {
        Paused = false;
        Time.timeScale = 1;
    }
}
