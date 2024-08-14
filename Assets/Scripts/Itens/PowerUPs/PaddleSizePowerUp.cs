using UnityEngine;

//Power-up que aumenta o tamano da pá
public class PaddleSizePowerUp : IPowerUp
{
    //Novo tamanho
    private float newWidth;
    //Referência do player
    private Player player;
    //Tamanho inicial
    private Vector3 startWidth;

    public PaddleSizePowerUp(float newWidth, Player player)
    {
        this.newWidth = newWidth;
        this.player = player;
        startWidth = this.player.transform.localScale;
    }

    //Aumenta o tamanho do player
    public void Active()
    {
        player.transform.localScale = new Vector3(newWidth, player.transform.localScale.y, player.transform.localScale.z);
    }

    //Volta o tamanho do player para o inicial
    public void Deactivate()
    {
        player.transform.localScale = startWidth;
    }
}
