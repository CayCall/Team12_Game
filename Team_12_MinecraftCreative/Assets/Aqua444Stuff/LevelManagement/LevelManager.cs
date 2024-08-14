using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject PauseScreen;

    private void Update()
    {
        //Pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    //Completely Reset the Level
    public void ResetLevel()
    {
        SceneManager.LoadScene("GameScene"); //change to appropriate name
        Time.timeScale = 1.0f;
    }

    //Completely quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    //Resume from PauseScreen
    public void ResumeGame()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1.0f;
    }

    //Return to the Start Screen
    public void ReturnToStart()
    {
        SceneManager.LoadScene("StartScreen"); //change to appropriate name
    }
}
