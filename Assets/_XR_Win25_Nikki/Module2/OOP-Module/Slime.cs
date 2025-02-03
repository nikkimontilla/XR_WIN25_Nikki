using System;
using System.Collections;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private float m_moveSpeed = 0.5f;

    private void Update()
    {
        if (IsWithinAttackRange)
            HandleAttack();
        else
            MoveTowardsPlayer();
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

        attackCoroutine = null; // Reset the coroutine reference when the slime moves out of range
    }

    private void Attack()
    {
        Debug.Log($"{Name} is attacking with {m_attackDamage} damage!");
    }

    private void MoveTowardsPlayer()
    {
        if (m_playerTarget == null) return;

        transform.position = Vector3.MoveTowards(transform.position, m_playerTarget.position, m_moveSpeed * Time.deltaTime);
    }
}
