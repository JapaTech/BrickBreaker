using System.Collections;
using UnityEngine;

public class Enemy : Brick, Attack
{
    //Refer�ncia de Componentes
    [SerializeField] private Projectile projctilePrefab;
    [SerializeField] private Transform shootPoint;

    //De quanto em quanto tempo o inimigo ir� atacar
    [SerializeField] private float attackRate;

    //Inscreve no evento de quando o proj�til for destru�do, pois o cooldown do ataque s� � dispirado quando
    //o inimigo n�o tem proj�til na tela

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

    //Lan�� o proj�til
    public void ExecuteAttack()
    {
        //Cria o proj�til
        Projectile projectile = Instantiate(projctilePrefab, shootPoint.position, shootPoint.rotation);
        
        //Diz ao proj�til que foi criado por este inimgo
        projectile.creator = this;

        //Se for um proj�til target, usa a fun��o passando a posi��o do player
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

    //Se o proj�til criado por esse inimigo sair do jogom cria um novo
    private void AttackAgain(Projectile projectile)
    {
        if (projectile.IsCreatedByEnemy(this))
        {
            StopCoroutine(AttackCooldown());
            StartCoroutine(AttackCooldown());
        }
    }
}
