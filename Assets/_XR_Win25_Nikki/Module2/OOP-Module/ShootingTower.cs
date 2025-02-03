using System.Collections;
using UnityEngine;

public class ShootingTower : Enemy
{

    [SerializeField] private Transform m_torret;
    [SerializeField] private Projectile m_projectilePrefab;

    private void Update()
    {
        if (IsWithinAttackRange)
            HandleAttack();
    }

    private void HandleAttack()
    {
        if (attackCoroutine == null)
            attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while (IsWithinAttackRange)
        {
            Attack();
            yield return new WaitForSeconds(m_attackRate);
        }

        attackCoroutine = null; // Reset the coroutine reference when the enemy moves out of range
    }

    private void Attack()
    {
        Debug.Log($"{Name} is attacking with {m_attackDamage} damage!");

        AimTorret(m_playerTarget);
        Fire();
    }

    private void AimTorret(Transform target)
    {
        // Get the direction from the object to the player
        Vector3 direction = m_playerTarget.position - transform.position;

        // Zero out the Y component to prevent rotation on the vertical axis
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            // Create a rotation towards the target while ignoring vertical rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }

    private void Fire()
    {
        Projectile projectile = Instantiate(m_projectilePrefab, m_torret.position, m_torret.rotation);
        projectile.Shoot(m_attackDamage);
    }

}
