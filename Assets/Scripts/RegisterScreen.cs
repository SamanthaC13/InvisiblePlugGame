using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;

public class RegisterScreen : MonoBehaviour
{
    public Button createButton;
    public Button backButton;
    public InputField user;
    public InputField fn;
    public InputField ln;
    public InputField pass;
    public InputField passC;
    public Text errorMessage;
    public string test;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("Start");
        createButton.onClick.AddListener(onCreateButtonPress);
        backButton.onClick.AddListener(onBackButtonPress);
        UnityEngine.Debug.Log("start finished");
    }
    IEnumerator GetText()
    {

        user = GameObject.Find("User").GetComponent<InputField>();
        fn = GameObject.Find("FirstName").GetComponent<InputField>();
        ln = GameObject.Find("LastName").GetComponent<InputField>();
        pass = GameObject.Find("Password").GetComponent<InputField>();
        passC = GameObject.Find("PasswordConfirm").GetComponent<InputField>();
        errorMessage = GameObject.Find("Error").GetComponent<Text>();

        UnityEngine.Debug.Log("Hello World");
        errorMessage.text = "in the function";

        if (user.text == "" || fn.text == "" || ln.text == "" || pass.text == "" || passC.text == "")
        {
            errorMessage.text = "Must fill in all fields";
        }
        else if (pass.text != passC.text)
        {
            errorMessage.text = "Passwords must match";
        }
        else
        {
            UnityWebRequest www = UnityWebRequest.Get("https://invisible-plug-game.herokuapp.com/register.php?username=" + user.text + "&password=" + pass.text + "&firstname=" + fn.text + "&lastname=" + ln.text);
            UnityEngine.Debug.Log(www.downloadHandler.text);
            yield return www.SendWebRequest();
            test = www.downloadHandler.text;
            if (test.Substring(0, 1) == "S")
            {
                SceneManager.LoadScene("Login");
            }
            else
            {
                errorMessage.text = "Username aleady in use";
            }
        }


        //if (www.result != UnityWebRequest.Result.Success)
        //{
        //Debug.Log(www.error);
        //}
        //else
        //{
        // Show results as text
        //UnityEngine.Debug.Log(www.downloadHandler.text);
        //test = www.downloadHandler.text;
        // Or retrieve results as binary data
        //test = www.downloadHandler.text;
        UnityEngine.Debug.Log(test);
        UnityEngine.Debug.Log(test.Substring(0, 1));

        //}
    }
    // Update is called once per frame
    void Update()
    {


    }

    public void onBackButtonPress()
    {
        SceneManager.LoadScene("Login");
    }

    public void onCreateButtonPress()
    {
        StartCoroutine(GetText());
    }
}
