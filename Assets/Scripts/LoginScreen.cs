using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters;

public class LoginScreen: MonoBehaviour
{
    public InputField x;
    public string test;
    public Button loginButton;
    public Button registerButton;
    public Button resetButton;
    public string user;
    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(onLoginButtonPress);
        registerButton.onClick.AddListener(onRegisterButtonPress);
        resetButton.onClick.AddListener(onResetButtonPress);
        x.onEndEdit.AddListener(usernameUpdate);
    }
    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://invisible-plug-game.herokuapp.com/login.php?username=" + user + "&password=Password");
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
        UnityEngine.Debug.Log(test.Substring(0,1));
        if (test.Substring(0, 1) == "1")
        {
            SceneManager.LoadScene("Menu");
        }

        //}
    }
    // Update is called once per frame
    void Update()
    {
        
    }




    public void usernameUpdate()
    {
        UnityEngine.Debug.Log("Hello World");
    }
    public void onLoginButtonPress()
    {
        UnityEngine.Debug.Log("Start trying to get to the database");
        user = x.text;
        StartCoroutine(GetText());
        
    }

    public void onRegisterButtonPress()
    {
        SceneManager.LoadScene("Register");
    }

    public void onResetButtonPress()
    {
        
    }
}
