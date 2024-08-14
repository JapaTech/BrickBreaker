using System.Collections;
using UnityEngine;

public class Enemy : Brick, Attack
{
    //Referência de Componentes
    [SerializeField] private Projectile projctilePrefab;
    [SerializeField] private Transform shootPoint;

    //De quanto em quanto tempo o inimigo irá atacar
    [SerializeField] private float attackRate;

    //Inscreve no evento de quando o projétil for destruído, pois o cooldown do ataque só é dispirado quando
    //o inimigo não tem projétil na tela

    private void OnEnable()
    {
        GameEvents.OnProjectileDeath += AttackAgain;
    }

    private void OnDisable()
    {
        GameEvents.OnProjectileDeath -= AttackAgain;
    }

    private void Start()
    {
        StartCoroutine(AttackCooldown());
    }

    //Lançã o projétil
    public void ExecuteAttack()
    {
        //Cria o projétil
        Projectile projectile = Instantiate(projctilePrefab, shootPoint.position, shootPoint.rotation);
        
        //Diz ao projétil que foi criado por este inimgo
        projectile.creator = this;

        //Se for um projétil target, usa a função passando a posição do player
        if(projectile is TargetProjectile)
        {
            projectile.Movement(GameManager.Instance.PlayerRef.transform);
        }
    }

    //Espera um tempo para atacar
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackRate);
        ExecuteAttack();
    }

    //Se o projétil criado por esse inimigo sair do jogom cria um novo
    private void AttackAgain(Projectile projectile)
    {
        if (projectile.IsCreatedByEnemy(this))
        {
            StopCoroutine(AttackCooldown());
            StartCoroutine(AttackCooldown());
        }
    }
}
