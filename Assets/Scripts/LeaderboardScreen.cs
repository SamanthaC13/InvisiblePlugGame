using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;

public class LeaderboardScreen : MonoBehaviour
{
    public Button backButton;
    string user = "";
    string[] stats = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""};
    string test = "";
    int place = 0;
    int size = 0;
    public Text msg;
    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(onBackButtonPress);
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        //num games play, win/loss, best time
        UnityWebRequest www = UnityWebRequest.Get("https://invisible-plug-game.herokuapp.com/leaders.php");
        yield return www.SendWebRequest();
        test = www.downloadHandler.text;
        for (int i = 0; i < test.Length; i++)
        {
            if (test.Substring(i, 1) == "," || test.Substring(i, 1) == "<")
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
        msg = GameObject.Find("Leaderboard").GetComponent<Text>();
        msg.text = "Player".PadRight(20) + "Reward Lvl".PadLeft(15) + "Best Time".PadLeft(15) + "W/L\n".PadLeft(25);
        for (int z = 0; z < 20; z = z + 4)
        {
            msg.text += "\n" + stats[z].PadRight(20) + " " + stats[z + 1].PadLeft(15) + " " + stats[z + 2].PadLeft(15) + " " + stats[z + 3].PadLeft(30);
        }
        /*int j = 0;
        int i = 0;
        place = 0;
        for (; i < test.Length; i++)
            {
            if (test.Substring(i, 1) == ",")
            {
                place++;
                if(place >= 4)
                {
                    j++;
                    place = 0;
                }
                size = 0;
            }
            else
            {
                stats[j, place] += test.Substring(i, 1);
            }
        }


        for(int x = 0; x < 5; x++)
        {
            for(int y = 0; y < 4; y++)
            {
                UnityEngine.Debug.Log(stats[x, y]);
            }
        }


        msg = GameObject.Find("Leaderboard").GetComponent<Text>();
        msg.text = "Player\tReward Lvl\tBest Time\tW/R";
        for (int z = 0; z < 5; z++)
        {
            msg.text += "\n" + stats[z, 0] + "\t" + stats[z, 1] + "\t" + stats[z, 2] + "\t" + stats[z, 3];
        }*/
    }


        // Update is called once per frame
        void Update()
    {
        
    }

    void onBackButtonPress() {
        SceneManager.LoadScene("Menu");
    }
}
