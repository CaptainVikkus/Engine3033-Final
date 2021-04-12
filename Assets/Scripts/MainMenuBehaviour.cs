using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionMenu;
    public GameObject controlMenu;

    private void Start()
    {
        OpenMenu();
    }

    public void OpenOption()
    {
        optionMenu.SetActive(true);

        mainMenu.SetActive(false);
        controlMenu.SetActive(false);
    }

    public void OpenMenu()
    {
        mainMenu.SetActive(true);

        optionMenu.SetActive(false);
        controlMenu.SetActive(false);
    }
    public void OpenControl()
    {
        controlMenu.SetActive(true);

        optionMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void OpenLevel(int build)
    {
        SceneManager.LoadScene(build);
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
