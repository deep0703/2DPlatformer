using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletFarm : MonoBehaviour
{
    [Header("Bullet Production")]
    [SerializeField] private int maxBullets = 100;
    [SerializeField] private int currentBullets = 0; // You can start with some bullets already in the farm.
    [SerializeField] private float productionRate = 0.01f; // Bullets produced per second.

    [Header("UI")]
    [SerializeField] private TMP_Text bulletCounterText;

    private float timeSinceLastProduction;

    private void Update()
    {
        ProduceBullets();
        UpdateBulletCounter();
    }

    private void ProduceBullets()
    {
        timeSinceLastProduction += Time.deltaTime;

        if (timeSinceLastProduction >= 1f / productionRate)
        {
            currentBullets++;
            timeSinceLastProduction = 0f;
        }

        currentBullets = Mathf.Clamp(currentBullets, 0, maxBullets);
    }

    private void UpdateBulletCounter()
    {
        bulletCounterText.text = "Bullet Farm:" + currentBullets.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Turret"))
        {
            Turret turret = other.gameObject.GetComponent<Turret>();
            int bulletsNeeded = turret.MaxBullets - turret.CurrentBullets;
            int bulletsToGive = Mathf.Min(bulletsNeeded, currentBullets);

            turret.AddBullets(bulletsToGive);
            currentBullets -= bulletsToGive;
        }
    }
}
