using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void RetryScene()
    {
        GameController.Instance.ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToTitleScreen()
    {
        SceneManager.LoadScene(0);
    }    
    
    public void GoToCredits()
    {
        SceneManager.LoadScene(2);
    }



}
