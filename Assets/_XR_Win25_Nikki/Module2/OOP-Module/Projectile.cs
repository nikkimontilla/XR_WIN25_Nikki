using UnityEngine;
using UnityEngine.Pool;

[RequireComponent (typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody m_rb;
 
    [SerializeField] float m_speed = 1f;  
    [SerializeField] int m_damagePower= 5;

    IObjectPool<Projectile> m_projectilePool;

    public void SetPool(IObjectPool<Projectile> pool)
    {
        m_projectilePool = pool;
    }
    void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity =false;
    }
    void OnCollisionEnter(Collision collision)
    {
        // Use object pooling. Return them to pool
        //Destroy(gameObject);

        m_projectilePool.Release(this);
    }
    public void Shoot(int damagePower)
    {
        m_damagePower = damagePower;

        m_rb.linearVelocity = transform.forward * m_speed;
    }
}

