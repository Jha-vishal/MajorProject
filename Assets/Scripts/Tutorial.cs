

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    [SerializeField]
   GameObject levelController;
   [SerializeField]
   GameObject pauseMenu;
   public bool isLevelCleared = false;

    void Start()
    {
        levelController = GameObject.FindWithTag("LevelController");
        pauseMenu.SetActive(false);
        
    }


    void Update()
    {
        
        Rotate();
    }


    void Rotate()
    {
        this.gameObject.transform.Rotate(0, 1, 0);
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
    
            Debug.Log("level Cleared");

            levelController.GetComponent<LevelController>().levelStatus.text = "Mission Passed";
            isLevelCleared = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);

        }
        else if (col.gameObject.tag == "Enemy")
        {

            levelController.GetComponent<LevelController>().levelStatus.text = "Mission Failed";
            levelController.GetComponent<LevelController>().levelStatus.color = Color.red;
            isLevelCleared = false;
            
            // Restart Level
            levelController.GetComponent<LevelController>().RestartLevel();
        }
    }
}
