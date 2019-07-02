using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadTutorial1()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadTutorial2()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadTutorial3()
    {
        SceneManager.LoadScene(5);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
