using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{ 
    public void ClickedPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickedInstrucions()
    {
        SceneManager.LoadScene(2);
    }

    public void ClickedCredits()
    {
        SceneManager.LoadScene(3);
    }

    public void ClickedQuit()
    {
        Application.Quit();
        
    }

    public void CickedBack()
    {
        SceneManager.LoadScene(0);
    }

}
