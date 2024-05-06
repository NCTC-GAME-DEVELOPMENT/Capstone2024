using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPanelManager : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject characterPanel;
    [SerializeField] GameObject creditsPanel;
    


    public void StartButton()
    {
        SceneManager.LoadScene("Testing");
        //mainPanel.SetActive(false);
        //characterPanel.SetActive(true);
    }

    public void CreditsButton()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackToMenuButton()
    {
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }



    //character select menu

    //public void StartCharacterButton()
    //{
    //    characterPanel.SetActive(false);
    //    //move to level select
    //}

    //public void CharacterBackButton()
    //{
    //    characterPanel.SetActive(false);
    //    mainPanel.SetActive(true);
    //}
    
}
