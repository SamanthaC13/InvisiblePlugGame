using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginScreen: MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onLoginButtonPress() {
        SceneManager.LoadScene("Menu");
    }

    public void onRegisterButtonPress()
    {
        SceneManager.LoadScene("Register");
    }

    public void onResetButtonPress()
    {
        
    }
}
