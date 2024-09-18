using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{

    public Image[] dashUpgrades;
    public Image[] hpUpgrades;
    public Image[] damageUpgrades;
    public Image[] speedUpgrades;

    public int dashLevel = 0;
    public int hpLevel = 0;
    public int damageLevel = 0;
    public int speedLevel = 0;

    private Color myGrey = new Color(164, 164, 164, 255);
    

    public TextMeshProUGUI totalMemText;

    private int totalMemory;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("TotalMemory", 10000);
        totalMemory = PlayerPrefs.GetInt("TotalMemory", 0);
        totalMemText.text = "MEMORY: " + totalMemory;
        dashLevel = PlayerPrefs.GetInt("dashLevel", 0);
        hpLevel = PlayerPrefs.GetInt("hpLevel", 0);
        damageLevel = PlayerPrefs.GetInt("damageLevel", 0);
        speedLevel = PlayerPrefs.GetInt("speedLevel", 0);
        SetLevels();
        
    }

    public void MainMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void SetLevels() {
        for (int i = 0; i < dashUpgrades.Length; i++) {
            if (i <= dashLevel - 1) {
                dashUpgrades[i].color = Color.green;
            } else {
                dashUpgrades[i].color = Color.grey;
            }
        } 
        for (int i = 0; i < hpUpgrades.Length; i++) {
            if (i <= hpLevel - 1) {
                hpUpgrades[i].color = Color.green;
            } else {
                hpUpgrades[i].color = Color.grey;
            }
        }
        for (int i = 0; i < damageUpgrades.Length; i++) {
            if (i <= damageLevel - 1) {
                damageUpgrades[i].color = Color.green;
            } else {
                damageUpgrades[i].color = Color.grey;
            }
        }
        for (int i = 0; i < speedUpgrades.Length; i++) {
            if (i <= speedLevel - 1) {
                speedUpgrades[i].color = Color.green;
            } else {
                speedUpgrades[i].color = Color.grey;
            }
        }
    }

    public void ResetUpgrades() {
        int refundedPoints = 0;
        for (int i = 0; i < dashLevel; i++) {
            refundedPoints += 1000 * (int)Mathf.Pow(2, i);
        }
        for (int i = 0; i < hpLevel; i++) {
            refundedPoints += 1000 * (int)Mathf.Pow(2, i);
        }
        for (int i = 0; i < damageLevel; i++) {
            refundedPoints += 1000 * (int)Mathf.Pow(2, i);
        }
        for (int i = 0; i < speedLevel; i++) {
            refundedPoints += 1000 * (int)Mathf.Pow(2, i);
        }

        PlayerPrefs.SetInt("dashLevel", 0);
        dashLevel = 0;
        PlayerPrefs.SetInt("hpLevel", 0);
        hpLevel = 0;
        PlayerPrefs.SetInt("damageLevel", 0);
        damageLevel = 0;
        PlayerPrefs.SetInt("speedLevel", 0);
        speedLevel = 0;
        SetLevels();
        totalMemory += refundedPoints;
        PlayerPrefs.SetInt("TotalMemory", totalMemory);
        totalMemText.text = "MEMORY: " + totalMemory;
    }

    public void UpgradeDash() {
        Upgrade(ref dashLevel, dashUpgrades);
    }

    public void UpgradeHP() {
        Upgrade(ref hpLevel, hpUpgrades);
    }

    public void UpgradeDamage() {
        Upgrade(ref damageLevel, damageUpgrades);
    }
    public void UpgradeSpeed() {
        Upgrade(ref speedLevel, speedUpgrades);
    }

    private void Upgrade(ref int level, Image[] upgrades) {
        int[] costs = { 1000, 2000, 4000, 8000 };
        
        if (level < costs.Length && totalMemory >= costs[level]) {
            totalMemory -= costs[level];
            totalMemText.text = "MEMORY: " + totalMemory;
            upgrades[level].color = Color.green;
            level++;
            
            PlayerPrefs.SetInt("TotalMemory", totalMemory);
            PlayerPrefs.SetInt(upgrades == dashUpgrades ? "dashLevel" : 
                            upgrades == hpUpgrades ? "hpLevel" : 
                            upgrades == damageUpgrades ? "damageLevel" :
                            upgrades == speedUpgrades ? "speedLevel" : "", level);
        }
    }


    
}
