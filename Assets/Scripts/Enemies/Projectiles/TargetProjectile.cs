using UnityEngine;

public class TargetProjectile : Projectile
{
    //Dire��o que o proj�til deve ir
    private Vector3 direction;

    //Atira o proj�til na dire��o do jogador
    public override void Movement(Transform target)
    {
        direction = (target.position - tr.position).normalized;
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Se atingir o jogador causa dano no jogador e no proj�til
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.ChangeHealth(-damage);
            ChangeHealth(-1);
        }
        //Se atingir a zona da morte causa dano no proj�til
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
