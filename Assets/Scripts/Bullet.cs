using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    // Bullet Limitation
    private static int currentBulletCount = 0;
    private const int MAX_BULLETS = 10;  // Modify this value based on your desired limit

    private Transform target;

    public static bool CanInstantiateBullet()
    {
        return currentBulletCount < MAX_BULLETS;
    }

    private void Awake()
    {
        currentBulletCount++;
    }

    private void OnDestroy()
    {
        currentBulletCount--;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        rb.velocity = direction * bulletSpeed;
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }
}
