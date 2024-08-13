using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProjectile : Projectile
{
    private Vector3 direction;

    public override void Movement(Transform target)
    {
        direction = (target.position - tr.position).normalized;
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.ChangeHealth(-damage);
            ChangeHealth(-1);
        }
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
