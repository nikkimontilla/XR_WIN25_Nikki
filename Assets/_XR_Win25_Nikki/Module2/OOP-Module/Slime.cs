using System;
using System.Collections;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private float m_moveSpeed = 0.5f;

    //Moved to Enemy
    protected override void Update()
    {
        base.Update();

        if (!IsWithinAttackRange)
        {
            MoveTowardsPlayer();
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    //Moved to Enemy
    protected override IEnumerator AttackCoroutine() => base.AttackCoroutine();

    //Stay in Slime
    private void MoveTowardsPlayer()
    {
        if (m_playerTarget == null) return;

        transform.position = Vector3.MoveTowards(transform.position, m_playerTarget.position, m_moveSpeed * Time.deltaTime);
    }

    //Moved to Enemy
    protected override void Attack()
    {
        base.Attack();
    }
}
