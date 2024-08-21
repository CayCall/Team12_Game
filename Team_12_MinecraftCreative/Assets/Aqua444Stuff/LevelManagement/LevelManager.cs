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
        SceneManager.LoadScene("SampleScene"); //change to appropriate name --> // Change to Sample Scene
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
        SceneManager.LoadScene("MainMenu"); //change to appropriate name   --> //changed to main menu
    }

    public void ViewCredits()
    {
        SceneManager.LoadScene("Credits"); // Load our credits scene
    }
}
