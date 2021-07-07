
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class LevelController : MonoBehaviourPunCallbacks
{


    [Header("Destination")]
    public GameObject destPrefab;
    public Transform destSpawnPoint;
    GameObject destination;


    [Header("Player Car")]
    int currentCar = 0;
    public GameObject[] playerCarPrefab;
    GameObject playerCar = null;


    [Header("Enemy Car")]
    public GameObject enemyCarPrefab;

    GameObject enemyCar;

    [Header("Level Controlling")]
    public Transform[] vehicleSpawnPoint;
    public Animator transition;
    public float transitionTime = 2f;

    int indexOfCurrentLevel;

    public Text levelStatus;

    Timer timer;


    void Init()
    {
        // Instance of destination
        destination = Instantiate(destPrefab);
        destination.transform.position = destSpawnPoint.transform.position;
        destination.transform.rotation = destSpawnPoint.transform.rotation;
    }


    void Spawning()
    {
        int randomPos = Random.Range(0, vehicleSpawnPoint.Length);
        // for SinglePlayer Car pos & rot
        Vector3 startPos = vehicleSpawnPoint[randomPos].position;
        Quaternion startRot = vehicleSpawnPoint[randomPos].rotation;

        // for multiplayer car pos & rot
        //Vector3 mStartPos;
        //Quaternion mStartRot;

        //int[] position = new int[4];



        if (PhotonNetwork.IsConnected)
        {
            startPos = vehicleSpawnPoint[PhotonNetwork.CurrentRoom.PlayerCount - 1].position;
            startRot = vehicleSpawnPoint[PhotonNetwork.CurrentRoom.PlayerCount - 1].rotation;

            if (NetworkedPlayer.localPlayerInstance == null)
            {
                playerCar = PhotonNetwork.Instantiate(playerCarPrefab[currentCar].name, startPos, startRot, 0);
            }

            if (PhotonNetwork.IsMasterClient)
            {
                //startButton.SetActive(true);
            }
            else
            {
                //waitingText.SetActive(true);
            }
        }

        else
        {
            playerCar = Instantiate(playerCarPrefab[currentCar]);

            playerCar.transform.position = startPos;
            playerCar.transform.rotation = startRot;

            foreach(Transform t in vehicleSpawnPoint)
            {
                if(t == vehicleSpawnPoint[randomPos])
                {
                    continue;
                }

                enemyCar = Instantiate(enemyCarPrefab);

                enemyCar.transform.position = t.position;
                enemyCar.transform.rotation = t.rotation;

            }
        }
    }



    void Start()
    {

        timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        Debug.Log(timer.totalTime);
        indexOfCurrentLevel = SceneManager.GetActiveScene().buildIndex;

        //startButton.SetActive(false);
        Init();


        if (PlayerPrefs.HasKey("SelectedCar"))
        {
            currentCar = PlayerPrefs.GetInt("SelectedCar");
        }

        Spawning();

        playerCar.GetComponent<Car>().enabled = true;
        playerCar.GetComponent<PlayerController>().enabled = true;

    }


    void Update()
    {
        if (timer.GetComponent<Timer>().totalTime <= 0)
        {
            RestartLevel();
            levelStatus.text = "Time Up";
        }
    }


    public void RestartLevel()
    {
        StartCoroutine(LoadLevel(indexOfCurrentLevel));
    }


    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(indexOfCurrentLevel + 1));
    }


    IEnumerator LoadLevel(int levelLoader)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelLoader);
    }


    public void LevelSelectMenu()
    {
        SceneManager.LoadScene("LevelSelect");
    }



}

