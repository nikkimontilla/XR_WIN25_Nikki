using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Transform m_playerTarget;
    [SerializeField] public float m_attackRate = 3f;
    [SerializeField] public float m_attackRange = 0.5f;
    [SerializeField] protected int m_attackDamage = 1;

    [field: SerializeField] public string Name { get; protected set; }

    [SerializeField] public Coroutine attackCoroutine;

    [SerializeField] public bool IsWithinAttackRange => Vector3.Distance(transform.position, m_playerTarget.position) < m_attackRange;

    protected virtual void Update()
    {
        if (IsWithinAttackRange)
            HandleAttack();
    }

    protected virtual void Awake()
    {
        if (m_playerTarget == null) m_playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Gizmo to draw the attack range as a green circle in the scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // Set the gizmo color to green

        // Number of segments to approximate the circle
        int segments = 50;
        float angleStep = 360f / segments;

        // Draw the circle
        Vector3 previousPoint = transform.position + new Vector3(m_attackRange, 0f, 0f); // Start at the right side of the circle
        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad; // Convert angle to radians
            Vector3 newPoint = transform.position + new Vector3(Mathf.Cos(angle) * m_attackRange, 0f, Mathf.Sin(angle) * m_attackRange);
            Gizmos.DrawLine(previousPoint, newPoint); // Draw line from the previous point to the new point
            previousPoint = newPoint; // Move to the new point for the next line
        }
    }

    public void HandleAttack()
    {
        if (attackCoroutine == null)
            attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    protected virtual void Attack()
    {
        Debug.Log($"{Name} is attacking with {m_attackDamage} damage!");
    }

    protected virtual IEnumerator AttackCoroutine()
    {
        while (IsWithinAttackRange)
        {
            Attack();
            yield return new WaitForSeconds(m_attackRate);
        }

        attackCoroutine = null; // Reset the coroutine reference when the enemy moves out of range
    }

}
