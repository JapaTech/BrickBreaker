using UnityEngine;

//Power Up que aumenta o tamanho da bola
public class BallSizePowerUp : IPowerUp
{
    //Dano inicial da bola
    private int startDamage;
    //Novo dano
    private int newDamage;
    //Multiplicador do tamanho da bola
    private float sizeMultiplier;
    //Tamanho que a bola iniciou
    private Vector3 startSize;
    //Referência para a bola
    private Ball ball;

    //Construtor
    public BallSizePowerUp(float sizeMultiplier, Ball ball, int damage)
    {
        this.sizeMultiplier = sizeMultiplier;
        this.ball = ball;
        startDamage = ball.Damage;
        newDamage = damage;
        startSize = ball.transform.localScale;
    }

    //Aumenta a bola e o dano
    public void Active()
    {
        ball.transform.localScale = ball.transform.localScale * sizeMultiplier;
        ball.Damage = newDamage;
    }

    //Volta a bola para o tamanho e  dano original
    public void Deactivate()
    {
        ball.transform.localScale = startSize;
        ball.Damage = startDamage;
    }
}
