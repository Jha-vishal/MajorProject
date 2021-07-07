using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject MainUI;
    public GameObject optionsMenu;
    //public Button closeBtn;
    public static bool isPaused=true;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }


    }

    public void Pause()
    {
        Time.timeScale = 0f;
        MainUI.SetActive(false);
        optionsMenu.SetActive(true);
        isPaused = true;

    }


    public void Resume()
    {
        Time.timeScale = 1f;
        MainUI.SetActive(true);
        optionsMenu.SetActive(false);
        isPaused = true;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
