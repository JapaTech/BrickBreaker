//Power-up da velocidade da p�
public class PaddleSpeedPowerUp : IPowerUp
{
    //Velocidade da nova da p�
    private float speed;
    //Refer�ncia do jogador
    private Player player;
    //Velocidade inicial do jogador
    private float startSpeed;

    //Construtor
    public PaddleSpeedPowerUp(float speed, Player player)
    {
        this.speed = speed;
        this.player = player;
        startSpeed = player.Speed;
    }

    //Muda a velocidade do jogador
    public void Active()
    {
        player.Speed = speed;
    }

    //Volta a velocidade para o valor inicial
    public void Deactivate()
    {
        player.Speed = startSpeed;
    }
}
