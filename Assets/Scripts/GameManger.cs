using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour {

    private static bool GameManagerExists;
   // private static string currentScene;
    public bool paused = false;
    public GameObject SettingsMenue;

    public string helpLineKey = "HelpLine";
    public string phoneNumber = "";
    private int helpLineGiven;
    public GameObject HelpLineSetUpObject;
    public GameObject HelpLineFailText;

    private void Start()
    {
        helpLineGiven = 0;
        helpLineGiven = PlayerPrefs.GetInt("helpLineGiven");
        if (helpLineGiven == 0)
        {
            initHelpLineNumber();
        }

        if (!GameManagerExists)
        {
            GameManagerExists = true;
            DontDestroyOnLoad(this.gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

  
    public void changeScene(string sceneToChangeTo)
    {
       // currentScene = sceneToChangeTo;
        SceneManager.LoadScene(sceneToChangeTo);
    }

    public void LeaveCloudMessage(GameObject cloud)
    {
        Time.timeScale = 0;
        //move cloud into focous
        
        CloudBehaviour cloudBehaviour = cloud.GetComponent<CloudBehaviour>();
        string note = InputText();
        cloudBehaviour.CloudText.text = note;
        cloudBehaviour.CloudText.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    public string InputText()
    {
        string inputText = "";
        TouchScreenKeyboard keyboard;
        keyboard = TouchScreenKeyboard.Open(inputText, TouchScreenKeyboardType.Default);
        return inputText;
    }

    private void initHelpLineNumber()
    {
        Time.timeScale = 0;
        HelpLineSetUpObject.SetActive(true);
        HelpLineFailText.SetActive(false);
    }

    public void SetHelpLineNumber()
    {
        InputField inputField = HelpLineSetUpObject.transform.GetComponentInChildren<InputField>();

        int number = 0;
        phoneNumber = inputField.text;
        if (int.TryParse(phoneNumber, out number))
        {
            Time.timeScale = 1;
            PlayerPrefs.SetInt("helpLineGiven",1);
            PlayerPrefs.SetString(helpLineKey, "tel: " + phoneNumber);
            HelpLineSetUpObject.SetActive(false);
        }
        else
        {
            HelpLineFailText.SetActive(true);
            //breakOutCase
            //PlayerPrefs.SetInt("helpLineGiven",0);
        }
    }

    public void Pause()
    {
        if(paused)
        {
            Time.timeScale = 1;
            SettingsMenue.SetActive(false);
            paused = false;
        }
        else
        {
            Time.timeScale = 0;
            SettingsMenue.SetActive(true);
            paused = true;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
