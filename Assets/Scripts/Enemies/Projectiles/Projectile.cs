using UnityEngine;

//Classe abstrata base para o proj�til
public abstract class Projectile : MonoBehaviour
{
    //Velocidade que o proj�til viaja
    [SerializeField] protected float speed;
    //Vida do proj�til
    [SerializeField] protected int health;
    //Dano que o proj�til causa
    [SerializeField] protected int damage;

    //Refer�cuna de componentes
    protected Rigidbody2D rb;
    protected Transform tr;
    [HideInInspector] public Enemy creator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = transform;
    }

    //Move o proj�til
    public abstract void Movement();
    public abstract void Movement(Transform target);

    //Atualiza a vida do proj�til
    protected virtual void ChangeHealth(int value)
    {
        health += value;

        if(health <= 0)
        {
            health = 0;
            Death();
        }
    }

    //Desativa o proj�til
    protected virtual void Death()
    {
        GameEvents.ProjectileDeath(this);
        
        gameObject.SetActive(false);
    }

    //Define quem foi que criou esse proj�til
    public bool IsCreatedByEnemy(Enemy enemy)
    {
        return enemy == creator ;
    }

}
