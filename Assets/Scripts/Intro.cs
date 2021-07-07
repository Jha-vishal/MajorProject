using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public float transitionTime = 3f;
    int MainMenu;

    void Start()
    {
        MainMenu = SceneManager.GetActiveScene().buildIndex + 1;
        
    }

    private void Update()
    {
        StartCoroutine(LoadLevel(MainMenu));
    }


    IEnumerator LoadLevel(int levelLoader)
    {
        
        //transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelLoader);

    }





}

