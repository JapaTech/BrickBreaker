using UnityEngine;

public class BallSizePowerUp : IPowerUp
{
    private float sizeMultiplier;
    private Vector3 startSize;
    private Ball ball;

    public BallSizePowerUp(float sizeMultiplier, Ball ball)
    {
        this.sizeMultiplier = sizeMultiplier;
        this.ball = ball;
        startSize = ball.transform.localScale;
    }

    public void Active()
    {

        ball.transform.localScale = ball.transform.localScale * sizeMultiplier;
    }

    public void Deactivate()
    {
        ball.transform.localScale = startSize;
    }
}
