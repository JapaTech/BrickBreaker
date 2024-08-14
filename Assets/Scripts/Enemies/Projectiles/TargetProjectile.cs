using UnityEngine;

public class TargetProjectile : Projectile
{
    //Direção que o projétil deve ir
    private Vector3 direction;

    //Atira o projétil na direção do jogador
    public override void Movement(Transform target)
    {
        direction = (target.position - tr.position).normalized;
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Se atingir o jogador causa dano no jogador e no projétil
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.ChangeHealth(-damage);
            ChangeHealth(-1);
        }
        //Se atingir a zona da morte causa dano no projétil
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Death"))
        {
            ChangeHealth(-1);
        }
    }

    public override void Movement()
    {
        throw new System.NotImplementedException();
    }
}
