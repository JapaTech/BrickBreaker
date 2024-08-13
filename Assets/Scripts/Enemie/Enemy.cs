using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Brick, Attack
{
    [SerializeField] Projectile projctilePrefab;
    [SerializeField] Transform shootPoint;
    [SerializeField] private float attackRate;

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

    public void ExecuteAttack()
    {
        Projectile projectile = Instantiate(projctilePrefab, shootPoint.position, shootPoint.rotation);
        projectile.creator = this;

        if(projectile is TargetProjectile)
        {
            projectile.Movement(PowerUpManager.Instance.player.transform);
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackRate);
        ExecuteAttack();
    }

    private void AttackAgain(Projectile projectile)
    {
        if (projectile.IsCreatedByEnemy(this))
        {
            StopCoroutine(AttackCooldown());
            StartCoroutine(AttackCooldown());
        }
    }
}
