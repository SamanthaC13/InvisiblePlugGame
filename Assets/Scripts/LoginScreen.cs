using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Diagnostics;



public class LoginScreen: MonoBehaviour
{
    public string test;
    public Button loginButton;
    public Button registerButton;
    public Button resetButton;
    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(onLoginButtonPress);
        registerButton.onClick.AddListener(onRegisterButtonPress);
        resetButton.onClick.AddListener(onResetButtonPress);
        
    }
    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://invisible-plug-game.herokuapp.com/login.php?username=ROC&password=Password");
        yield return www.SendWebRequest();
        //if (www.result != UnityWebRequest.Result.Success)
        //{
        //Debug.Log(www.error);
        //}
        //else
        //{
        // Show results as text
            UnityEngine.Debug.Log(www.downloadHandler.text);
            //test = www.downloadHandler.text;
            // Or retrieve results as binary data
            test = www.downloadHandler.text;
        UnityEngine.Debug.Log(test);
        //}
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void onLoginButtonPress()
    {
        UnityEngine.Debug.Log("Start trying to get to the database");
        StartCoroutine(GetText());
        UnityEngine.Debug.Log(test);
        if(test.Substring(1, 1) == "1"){
            SceneManager.LoadScene("Menu");
        }
        
    }

    public void onRegisterButtonPress()
    {
        SceneManager.LoadScene("Register");
    }

    public void onResetButtonPress()
    {
        
    }
}
