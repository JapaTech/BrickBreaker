using UnityEngine;

//Classe abstrata base para o projétil
public abstract class Projectile : MonoBehaviour
{
    //Velocidade que o projétil viaja
    [SerializeField] protected float speed;
    //Vida do projétil
    [SerializeField] protected int health;
    //Dano que o projétil causa
    [SerializeField] protected int damage;

    //Referêcuna de componentes
    protected Rigidbody2D rb;
    protected Transform tr;
    [HideInInspector] public Enemy creator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = transform;
    }

    //Move o projétil
    public abstract void Movement();
    public abstract void Movement(Transform target);

    //Atualiza a vida do projétil
    protected virtual void ChangeHealth(int value)
    {
        health += value;

        if(health <= 0)
        {
            health = 0;
            Death();
        }
    }

    //Desativa o projétil
    protected virtual void Death()
    {
        GameEvents.ProjectileDeath(this);
        
        gameObject.SetActive(false);
    }

    //Define quem foi que criou esse projétil
    public bool IsCreatedByEnemy(Enemy enemy)
    {
        return enemy == creator ;
    }

}
