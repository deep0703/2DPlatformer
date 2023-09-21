using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public Text ammoDisplay;
    public Text goldDisplay;
    public int totalAmmo = 0;
    public int totalGold = 0;
    public int buildingCost = 10;

    // Start is called before the first frame update
    void Start()
    {
        ammoDisplay.text = "Ammo: ";
        // Updates Ammo UI
        UpdateUI();

        // Repeats this call every 1/2 second.
        InvokeRepeating("IncrementAmmo", .1f,.1f);
        InvokeRepeating("IncrementGold", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void UpdateUI()
    {
        ammoDisplay.text = "Ammo: " + totalAmmo.ToString();
        ammoDisplay.color = Color.black;

        goldDisplay.text = "Gold: " + totalGold.ToString();
        goldDisplay.color = Color.black;
    }

    // Code for managing ammo
    void IncrementAmmo()
    {
        totalAmmo++;
        UpdateUI();
    }

    public void giveAmmo(int amount)
    {
        totalAmmo -= amount;
        UpdateUI();
    }

    // Code for managing gold
    public void IncrementGold()
    {
        totalGold++;
        UpdateUI();
    }

    public void spendGold(int amount)
    {
        totalGold -= amount;
        UpdateUI();
    }
}
