using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterScreen : MonoBehaviour
{
    public Button backButton;
    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(onBackButtonPress);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBackButtonPress() {
        SceneManager.LoadScene("Login");
    }

    public void onCreateButtonPress()
    {
        SceneManager.LoadScene("Login");
    }
}
