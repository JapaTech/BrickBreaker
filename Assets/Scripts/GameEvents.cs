using System;

//Classe que concentra os eventos do jogo
public static class GameEvents 
{
    //Evento para avisar quando o player surge
    public static event Action<Player> OnPlayerSpawn;
    //Evento para avisar quando a bola surge
    public static event Action<Ball> OnBallSpawn;
    //Evento para avisar quando o projétil morre
    public static event Action<Projectile> OnProjectileDeath;

    //Disparo de quando o player surge 
    public static void PlayerSpawned(Player player) => OnPlayerSpawn?.Invoke(player);
    
    //Disparo de quando a bola surge 
    public static void BallSpawned(Ball ball) => OnBallSpawn?.Invoke(ball);
    
    //Disparo de quando o projétil morre
    public static void ProjectileDeath(Projectile projectile) => OnProjectileDeath?.Invoke(projectile);
}
