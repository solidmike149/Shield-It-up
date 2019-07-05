using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Canvas canvas;

    private bool paused;

    private void Awake()
    {
        //canvas = FindObjectOfType<Canvas>();

        paused = false;
    }

    private void Update()
    {
        Pause();
    }

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

    public void Pause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!paused)
            {
                canvas.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                canvas.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
            
        }
    }
}