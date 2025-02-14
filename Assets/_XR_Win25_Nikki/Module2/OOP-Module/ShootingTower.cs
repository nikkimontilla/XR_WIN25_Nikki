using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ShootingTower : Enemy
{

    [SerializeField] private Transform m_torret;
    [SerializeField] private Projectile m_projectilePrefab;

    IObjectPool<Projectile> m_projectilePool;

    protected override void Awake()
    {
        base.Awake();
        m_projectilePool = new ObjectPool<Projectile>(CreateBullet, OnGet, OnRelease, OnActionDestroy);
    }
    private Projectile CreateBullet()
    {
        Projectile projectile = Instantiate(m_projectilePrefab, m_torret.position, m_torret.rotation);
        // Set pool on projectile

        projectile.SetPool(m_projectilePool);
        return projectile;
        

    }

    private void OnGet(Projectile projectile)
    {
        projectile.gameObject.SetActive(true); 
        projectile.transform.SetPositionAndRotation(m_torret.position, m_torret.rotation);
    }

    private void OnRelease(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnActionDestroy(Projectile projectile)
    {
        Destroy(projectile);
    }

    //Stay here
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

    //Stay here
    private void Fire()
    {
        //Changed to Object pooling. Retrieve object form pool

        //Projectile projectile = Instantiate(m_projectilePrefab, m_torret.position, m_torret.rotation);

        Projectile projectile = m_projectilePool.Get();
        projectile.Shoot(m_attackDamage);

    }

    //Move to Enemy
    protected override void Attack()
    {
        base.Attack();
        AimTorret(m_playerTarget);
        Fire();
    }

}
