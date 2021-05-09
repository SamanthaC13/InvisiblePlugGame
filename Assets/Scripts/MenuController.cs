using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;


public class MenuController : MonoBehaviourPunCallbacks
{
    public Image award;
    string gameVersion = "1";
    string user;
    public Image Medal1;
    public Image Medal2;
    public Image Medal3;
    public Image Medal4;
    public Image Medal5;
    public Image Medal6;
    int score;

    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject controlPanel;

    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject progressLabel;


    IEnumerator GetText()
    {

        award = GameObject.Find("award").GetComponent<Image>();
        UnityWebRequest www = UnityWebRequest.Get("https://invisible-plug-game.herokuapp.com/reward.php?username=" + user);
        yield return www.SendWebRequest();
        score = Int16.Parse(www.downloadHandler.text);
        UnityEngine.Debug.Log(score);

        //grey
        if (score == 0)
        {
            award = Medal1;
        }
        //bornze
        else if (score <= 5)
        {
            award = Medal2;
        }
        //silver
        else if (score <= 10)
        {
            award = Medal3;
        }
        //gold
        else if (score <= 15)
        {
            award = Medal4;
        }
        //blue
        else if (score <= 20)
        {
            award = Medal5;
        }
        //green
        else
        {
            award = Medal6;
        }
    }

    void Awake()
    {
        user = PlayerPrefs.GetString("name");
        UnityEngine.Debug.Log(user);
        StartCoroutine(GetText());
        //print("MENU AWAKE");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //print("MENU LOADED");
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    // Starts game if player hits button
    public void onStartButtonPress()
    {
        GameManager.inEditor = false; //Sets inEditor to false, because when we are starting the game at the start screen, we are either running a full build of the game outside of the editor, or we are wanting to test the game in actual multiplayer.
        Connect();
    }
    
    public void onHelpButtonPress() {
        SceneManager.LoadScene("Help");
    }

    public void onStatsButtonPress()
    {
        SceneManager.LoadScene("Stats");
    }
    public void onLeaderboardButtonPress()
    {
        SceneManager.LoadScene("Leaderboard");
    }



    void Connect()
    {
        print("MENU CONNECTED");
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public override void OnConnectedToMaster()
    {
        //UnityEngine.Debug.Log("MENU OnConnectedToMaster() was called by PUN");
        PhotonNetwork.JoinRandomRoom();
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        //UnityEngine.Debug.LogWarningFormat("MENU OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //UnityEngine.Debug.Log("MENU OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        //UnityEngine.Debug.Log("MENU OnJoinedRoom() called by PUN. Now this client is in a room.");
        PhotonNetwork.LoadLevel("Game");
    }
}
