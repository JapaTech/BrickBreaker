using UnityEngine;


public class PaddleSizePowerUp : IPowerUp
{
    private float newWidth;
    private Player player;
    private Vector3 startWidth;

    public PaddleSizePowerUp(float newWidth, Player player)
    {
        this.newWidth = newWidth;
        this.player = player;
        startWidth = this.player.transform.localScale;
    }

    public void Active()
    {
        player.transform.localScale = new Vector3(newWidth, player.transform.localScale.y, player.transform.localScale.z);
    }

    public void Deactivate()
    {
        player.transform.localScale = startWidth;
    }
}
