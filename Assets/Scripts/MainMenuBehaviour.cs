using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionMenu;

    public void OpenOption()
    {
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    public void OpenMenu()
    {
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
    }

    public void OpenStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenQuit()
    {
        Application.Quit();
    }

    public void SetDifficulty(int difficulty)
    {
        LevelManager.Difficulty = difficulty;
    }
}
