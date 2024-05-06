using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanelManager : MonoBehaviour
{

    public static EndPanelManager reference; 

    PauseManager pauseManager;
    GameObject panel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject lossPanel;

    private void Awake()
    {
        reference = this; 
        pauseManager = GetComponent<PauseManager>();
    }

    public void LoseGame()
    {
        panel = lossPanel;
        OpenMenu();
    }

    public void WinGame()
    {
        panel = winPanel;
        OpenMenu();
    }

    public void CloseMenu()
    {
        pauseManager.UnPauseGame();
        panel.SetActive(false);
    }

    public void OpenMenu()
    {
        if (panel = winPanel) 
        { pauseManager.PauseGame(); }
        
        panel.SetActive(true);
    }

    public void MainMenuButton()
    {
        
        CloseMenu();
        SceneManager.LoadScene("MainMenu");
    }

    public void ReplayButton()
    {
        CloseMenu();
        SceneManager.LoadScene("Testing");
    }
}
