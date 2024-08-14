//Power-up da velocidade da pá
public class PaddleSpeedPowerUp : IPowerUp
{
    //Velocidade da nova da pá
    private float speed;
    //Referência do jogador
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
