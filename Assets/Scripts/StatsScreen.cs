using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;

public class StatsScreen : MonoBehaviour
{
    string user = "";
    string[] stats = {"", "", "", ""};
    string test = "";
    int place = 0;
    int size = 0;
    public Text msg;

    // Start is called before the first frame update
    void Start()
    {
        user = PlayerPrefs.GetString("name");
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        //num games play, win/loss, best time
        UnityWebRequest www = UnityWebRequest.Get("https://invisible-plug-game.herokuapp.com/userstats.php?username=" + user);
        yield return www.SendWebRequest();
        test = www.downloadHandler.text;
        for(int i = 0; i < test.Length; i++)
        {
            if(test.Substring(i, 1) == "," || test.Substring(i, 1) == "<")
            {
                place++;
                size = 0;
            }
            else
            {
                stats[place] += test.Substring(i, 1);
                size++;
            }
        }
        msg = GameObject.Find("Stats").GetComponent<Text>();
        msg.text = "Number of Games Played: " + stats[0] + "\nWin/Loss Ratio: " + stats[1] + "\nBest time: " + stats[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
