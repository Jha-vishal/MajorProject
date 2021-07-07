using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{

    public Button[] levelButton;
    int levelReached;

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    

    void Start()
    {
        //loadingScreen.SetActive(false);
    }


    void Update()
    {
        if (PlayerPrefs.HasKey("levelReached"))
        {
            levelReached = PlayerPrefs.GetInt("levelReached",1);
        }


        for (int i = 0; i < levelButton.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButton[i].interactable = false;
        }
    }


    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsychronously(sceneIndex));
    }

    // Loading Screen
    IEnumerator LoadAsychronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = progress * 100 + "%";
            
            yield return null;
        }
    }
}
