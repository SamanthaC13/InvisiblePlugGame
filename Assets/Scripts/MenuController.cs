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
using System.Runtime.Versioning;

public class MenuController : MonoBehaviourPunCallbacks
{
    public Button award1;
    public Sprite Image1;
    public Sprite Image2;
    public Sprite Image3;
    public Sprite Image4;
    public Sprite Image5;


    /*public Button award2;
    public Button award3;
    public Button award4;
    public Button award5;
    public Button award6;*/
    string gameVersion = "1";
    string user;
    public string score = "Hello World";


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
        UnityWebRequest www = UnityWebRequest.Get("https://invisible-plug-game.herokuapp.com/reward.php?username=" + user);
        yield return www.SendWebRequest();
        score = www.downloadHandler.text;
        //award = GameObject.Find("Award").GetComponent<Image>();
        UnityEngine.Debug.Log(score);
        //grey
        if (score.Substring(0, 1) == "1")
        {
            award1.GetComponent<Image>().sprite = Image1;


            /* award1.gameObject.SetActive(true);

             award2.gameObject.SetActive(false);

             award3.gameObject.SetActive(false);

             award4.gameObject.SetActive(false);

             award5.gameObject.SetActive(false);

             award6.gameObject.SetActive(false);*/
        }
        //bornze
        else if (score.Substring(0, 1) == "2")
        {
            award1.GetComponent<Image>().sprite = Image2;

            /*award1.gameObject.SetActive(false);

            award2.gameObject.SetActive(true);

            award3.gameObject.SetActive(false);

            award4.gameObject.SetActive(false);

            award5.gameObject.SetActive(false);

            award6.gameObject.SetActive(false);*/
        }
        //silver
        else if (score.Substring(0, 1) == "3")
        {
            award1.GetComponent<Image>().sprite = Image3;

            /*award1.gameObject.SetActive(false);

            award2.gameObject.SetActive(false);

            award3.gameObject.SetActive(true);

            award4.gameObject.SetActive(false);

            award5.gameObject.SetActive(false);

            award6.gameObject.SetActive(false);*/
        }
        //gold
        /*else if (score.Substring(0, 1) == "3")
        {
            award4.gameObject.SetActive(true);
        }
        //blue
        else if (score.Substring(0, 1) == "4")
        {
            award5.gameObject.SetActive(true);
        }
        //green
        else
        {
            award6.gameObject.SetActive(true);
        }*/
    }

    void Awake()
    {
        
        //print("MENU AWAKE");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //print("MENU LOADED");
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        user = PlayerPrefs.GetString("name");
        UnityEngine.Debug.Log(user);

        award1 = GameObject.Find("Award1").GetComponent<Button>();
        Image1 = Resources.Load<Sprite>("BronzeMedalAsset 3");
        Image2 = Resources.Load<Sprite>("SilverMedalAsset 2");
        Image3 = Resources.Load<Sprite>("GoldMedalAsset 1");
        Image4 = Resources.Load<Sprite>("BlueMedalAsset 4");
        Image5 = Resources.Load<Sprite>("GreenMedalAsset 5");
        

        /*award1 = GameObject.Find("Award1").GetComponent<Button>();
        //award1.gameObject.SetActive(false);

        award2 = GameObject.Find("Award2").GetComponent<Button>();
        //award2.gameObject.SetActive(false);

        award3 = GameObject.Find("Award3").GetComponent<Button>();
        //award3.gameObject.SetActive(false);

        award4 = GameObject.Find("Award4").GetComponent<Button>();
        //award4.gameObject.SetActive(false);

        award5 = GameObject.Find("Award5").GetComponent<Button>();
        //award5.gameObject.SetActive(false);
         
        award6 = GameObject.Find("Award6").GetComponent<Button>();
        //award6.gameObject.SetActive(false);*/

        StartCoroutine(GetText());
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
    public void onAwardButtonPress()
    {
        SceneManager.LoadScene("ButtonRewards");
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
