using UnityEngine;

public class PaddleSpeedCommand : IPowerUp
{
    private float speed;
    private Player player;
    private float startSpeed;

    public PaddleSpeedCommand(float speed, Player player)
    {
        this.speed = speed;
        this.player = player;
        startSpeed = player.Speed;
    }

    public void Active()
    {
        player.Speed = speed;
    }

    public void Deactivate()
    {
        player.Speed = startSpeed;
    }
}
