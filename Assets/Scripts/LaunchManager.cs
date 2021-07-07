using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Photon.Realtime;
using Photon.Pun;



public class LaunchManager : MonoBehaviourPunCallbacks
{
    
    public GameObject MultiplayerPanel;
    
    public InputField playerName;

    public GameObject[] playerCarPrefabs;
    public Transform playerCarSpawnPoint;
    int currentCar = 0;




    ///////////////////////////////// Setting up multiplayer (Properties) //////////////////////////////////////////////
    byte maxplayerPerRoom = 4;
    bool isConnecting;
    string gameVersion = "1";

    public Text feedback;

////////////////////////////////////// Setting up multiplayer(Methods) ///////////////////////////////////////////////

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

    }


    void Start()
    {
        if(PlayerPrefs.HasKey("SelectedCar"))
        {
            currentCar = PlayerPrefs.GetInt("SelectedCar");
        }
        

        GameObject playerCar = Instantiate(playerCarPrefabs[currentCar]);
        //playerCar.transform.localScale= new Vector3(1f,1f,1f);

        playerCar.transform.parent = playerCarSpawnPoint.transform;
        playerCar.transform.position = playerCarSpawnPoint.transform.position;

        //playerCar.transform.localScale = new Vector3(1f,1f,1f);

        Vector3 rot = new Vector3(0,90,0);
        playerCar.transform.rotation = Quaternion.Euler(rot);

        playerCar.GetComponent<PlayerController>().enabled = false;
        playerCar.GetComponent<Car>().enabled = false;

            
    }


    public void ConnectNetwork()
    {
        feedback.text = "";
        isConnecting = true;
        PhotonNetwork.NickName = playerName.text;

        if(PhotonNetwork.IsConnected)
        {
            feedback.text += "\nJoining Room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            feedback.text += "\nConnecting...";
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

       
    }

    /////////////////////////////// Network CallBacks ///////////////////////////////////////////

    public override void OnConnectedToMaster()
    {
        if(isConnecting)
        {
            feedback.text += "\nOn Connected To Master...";
            PhotonNetwork.JoinRandomRoom(); 
        }
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        feedback.text += "\nFailed to join Random Room";
        feedback.text += "\nCreating Room...";
        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = this.maxplayerPerRoom});
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        feedback.text += "\nDisconnected because" + cause;
        isConnecting = false;
    }


    public override void OnJoinedRoom()
    {
        feedback.text += "\n Joined Room with " + PhotonNetwork.CurrentRoom.PlayerCount + " Players";
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 6);
    }


    public void ConnectSingle()
    {
        SceneManager.LoadScene("LevelSelect");
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void CarSelectMenu()
    {
        SceneManager.LoadScene("CarSelectMenu");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

}
