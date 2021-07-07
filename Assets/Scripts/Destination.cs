using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destination : MonoBehaviour
{

    public bool playerHit = false;
    public bool enemyHit = false;
    GameObject levelController;
    public bool isLevelCleared = false;
    


    void Start()
    {
        levelController = GameObject.FindWithTag("LevelController");
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
            playerHit = true;
            // Level Cleared text
            isLevelCleared = true;

            PlayerPrefs.SetInt("levelReached", SceneManager.GetActiveScene().buildIndex);

            levelController.GetComponent<LevelController>().LoadNextLevel();

            levelController.GetComponent<LevelController>().levelStatus.text = "Mission Passed";

        }
        else if (col.gameObject.tag == "Enemy")
        {
            
            enemyHit = true;
            // mission failed
            isLevelCleared = false;

            levelController.GetComponent<LevelController>().levelStatus.text = "Mission Failed";
            levelController.GetComponent<LevelController>().levelStatus.color = Color.red;
            
            // Restart Level
            levelController.GetComponent<LevelController>().RestartLevel();
        }
    }
}
