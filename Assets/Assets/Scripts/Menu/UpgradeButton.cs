using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMPro.TextMeshProUGUI upgradeName;
    [SerializeField] TMPro.TextMeshProUGUI upgradeDescription;

    public void Set(UpgradeData upgradeData)
    {
        icon.sprite = upgradeData.icon;
        upgradeName.text = upgradeData.description;
        upgradeData.name = upgradeData.name;
    }

    internal void Clean()
    {
        icon.sprite = null;
        upgradeName.text = null;
        upgradeDescription.text = null;
    }
}
