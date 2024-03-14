using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    PauseManager pauseManager;

    [SerializeField] List<UpgradeButton> upgradeButtons;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
    }

    //need to add in pausing
    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        pauseManager.PauseGame();
        panel.SetActive(true);
        for(int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }

    public void Upgrade(int pressedButtonID)
    {
        Debug.Log("Player pressed :" + pressedButtonID.ToString());
    }

    public void ClosePanel()
    {
        pauseManager.UnPauseGame();
        panel.SetActive(false);
    }
}
