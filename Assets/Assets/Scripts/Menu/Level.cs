using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{


    int level = 1;
    int experience = 0;

    int TO_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }

    [SerializeField] List<UpgradeData> upgrades;

    [SerializeField] UpgradePanelManager upgradePanel;
    [SerializeField] LevelTesting levelTesting;
    private void Start()
    {
        levelTesting.SetLevelText(level);
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
    }

    public void CheckLevelUp()
    {
        if(experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        upgradePanel.OpenPanel(GetUpgrades(4));
        experience -= TO_LEVEL_UP;
        level += 1;
        levelTesting.SetLevelText(level);
    }

    //Level Up Test Button
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            experience += TO_LEVEL_UP;
            CheckLevelUp();
        }
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if (count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        for(int i = 0; i < count; i++)
        {
            upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
        }
        
        return upgradeList;
    }
}
