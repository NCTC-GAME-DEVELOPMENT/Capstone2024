using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanelManager : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject characterPanel;


    public void StartButton()
    {
        mainPanel.SetActive(false);
        characterPanel.SetActive(true);
    }

    
    
    //character select menu
    public void StartCharacterButton()
    {
        characterPanel.SetActive(false);
        //move to level select
    }

    public void CharacterBackButton()
    {
        characterPanel.SetActive(false);
        mainPanel.SetActive(true);        
    }
}
