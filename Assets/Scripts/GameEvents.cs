using System;

public static class GameEvents 
{
    public static event Action<Player> OnPlayerSpawn;
    public static event Action<Ball> OnBallSpawn;

    public static void PlayerSpawned(Player player) => OnPlayerSpawn?.Invoke(player);
    public static void BallSpawned(Ball ball) => OnBallSpawn?.Invoke(ball);
}
