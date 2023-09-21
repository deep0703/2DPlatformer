using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;  // Required namespace for Unity UI.
// If you're using TextMeshPro, uncomment the line below:
using TMPro;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float bps = 1f; //Bullets per Second
    [SerializeField] private float speed = 5f;
    [SerializeField] private int count = 10;

    [Header("UI")]
    //[SerializeField] private Text bulletCountText;  // For Unity's default UI system.
    // If you're using TextMeshPro, use the line below:
    [SerializeField] private TMP_Text bulletCountText;

    private Transform target;
    private float timeUntilFire;
    public int MaxBullets { get; private set; } = 10;  // Set this value to your desired max bullets.
    public int CurrentBullets { get; private set; }  // Current bullets in the turret.

    private void Start()
    {
        CurrentBullets = count;
        UpdateBulletCountUI();
    }

    private void Update()
    {
        MoveTurret();
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();
        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        if (CurrentBullets > 0)
        {
            GameObject bulletobj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
            Bullet bulletScript = bulletobj.GetComponent<Bullet>();
            bulletScript.SetTarget(target);
            CurrentBullets--;
            UpdateBulletCountUI(); // Update the bullet count display after shooting.
        }
    }


    private void UpdateBulletCountUI()
    {
        bulletCountText.text = "Bullets: " + CurrentBullets;
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
#endif

    private void MoveTurret()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        transform.position += new Vector3(moveX, moveY, 0) * speed * Time.deltaTime;
    }

    public void AddBullets(int amount)
    {
        CurrentBullets += amount;
        CurrentBullets = Mathf.Clamp(CurrentBullets, 0, MaxBullets);
        UpdateBulletCountUI(); // Update the bullet count display after adding bullets.
    }

}
