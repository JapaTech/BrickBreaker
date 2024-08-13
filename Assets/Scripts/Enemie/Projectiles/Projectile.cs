using System;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    protected Rigidbody2D rb;
    protected Transform tr;
    [HideInInspector] public Enemy creator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = transform;
    }

    public abstract void Movement();
    public abstract void Movement(Transform target);

    protected virtual void ChangeHealth(int value)
    {
        health += value;
        Debug.Log("Função abstrata" + health);
        if(health <= 0)
        {
            health = 0;
            Death();
        }
    }

    protected virtual void Death()
    {
        GameEvents.ProjectileDeath(this);
        Debug.Log("Quem me chamou");
        gameObject.SetActive(false);
    }

    public bool IsCreatedByEnemy(Enemy enemy)
    {
        return enemy == creator ;
    }

}
