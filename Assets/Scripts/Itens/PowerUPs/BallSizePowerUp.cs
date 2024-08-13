using UnityEngine;

public class BallSizePowerUp : IPowerUp
{
    private int startDamage;
    private int newDamage;
    private float sizeMultiplier;
    private Vector3 startSize;
    private Ball ball;

    public BallSizePowerUp(float sizeMultiplier, Ball ball, int damage)
    {
        this.sizeMultiplier = sizeMultiplier;
        this.ball = ball;
        startDamage = ball.Damage;
        newDamage = damage;
        startSize = ball.transform.localScale;
    }

    public void Active()
    {
        ball.transform.localScale = ball.transform.localScale * sizeMultiplier;
        ball.Damage = newDamage;
    }

    public void Deactivate()
    {
        ball.transform.localScale = startSize;
        ball.Damage = startDamage;
    }
}
