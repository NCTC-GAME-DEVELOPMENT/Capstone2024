using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    int level = 1;
    int experience = 0;
    [SerializeField] ExperienceBar experienceBar;

    int TO_LEVEL_UP
    {
        get
        {
            return level;
        }
    }

    [SerializeField] List<UpgradeData> upgrades;

    [SerializeField] UpgradePanelManager upgradePanel;
    [SerializeField] PlayerController playerController;


    List<UpgradeData> selectedUpgrades;
    [SerializeField] List<UpgradeData> acquiredUpgrades;

    WeaponManager weaponManager;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    private void Start()
    {
        experienceBar.SetLevelText(level);
        //experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
    }

    public void CheckLevelUp()
    {
        if(experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        if (selectedUpgrades == null) { selectedUpgrades = new List<UpgradeData>(); }
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(4));

        upgradePanel.OpenPanel(selectedUpgrades);
        experience -= TO_LEVEL_UP;
        level += 1;
        experienceBar.SetLevelText(level);
        //experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
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

        List<UpgradeData> unusedUpgradeList = new List<UpgradeData>();

        if (count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        for(int i = 0; i < upgrades.Count; i++)
        {
            unusedUpgradeList.Add(upgrades[i]);
        }

        for(int i = 0; i < count; i++)
        {
            int random = Random.Range(0, unusedUpgradeList.Count);
            upgradeList.Add(unusedUpgradeList[random]);
            //upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
            unusedUpgradeList.RemoveAt(random);
        }
        
        return upgradeList;
    }

    public void Upgrade(int selectedUpgradeId)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeId];

        if(acquiredUpgrades == null) { acquiredUpgrades = new List<UpgradeData>(); }

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(upgradeData);
                break;
            case UpgradeType.WeaponUnlock:
                weaponManager.AddWeapon(upgradeData.weaponData);
                break;
            case UpgradeType.ItemUpgrade: 
                break;
            case UpgradeType.ItemUnlock:
                break;
            case UpgradeType.SkillUpgrade:
                playerController.UpgradeStats(upgradeData.playerUpgradeStats);
                AddAvailableUpgrades(upgradeData.nextUpgradeData);
                break;
            case UpgradeType.SkillUnlock:
                break;
        }

        acquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }

    internal void AddAvailableUpgrades(List<UpgradeData> upgradesToAdd)
    {
        this.upgrades.AddRange(upgradesToAdd);
    }
}
