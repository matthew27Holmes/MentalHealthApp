using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour {

    private static bool GameManagerExists;

   // private static string currentScene;
    public bool paused = false;
    public settingManger SettingsMenue;

    public string helpLineKey = "HelpLine";
    public string phoneNumber = "";
    private int helpLineGiven;
    public GameObject HelpLineSetUpObject;
    public GameObject HelpLineFailText;
    private TouchScreenKeyboard keyboard;

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

    public void FixedUpdate()
    {
        Pause();
    }

    public void changeScene(string sceneToChangeTo)
    {
       // currentScene = sceneToChangeTo;
        SceneManager.LoadScene(sceneToChangeTo);
    }

    public void moveCloudToPostion(GameObject cloud, Vector3 playerPos)
    {
        paused = true;
        SettingsMenue.setCanBeOpened(false);
        CloudBehaviour cloudBehaviour = cloud.GetComponent<CloudBehaviour>();
        playerPos.z += 10; // need to get offset in realtion to direction
        cloudBehaviour.setCloudMove(cloud.transform.position, playerPos,true);
    }

    public void LeaveCloudMessage(CloudBehaviour cloudBehaviour)
    {
        string note = "";
        keyboard = TouchScreenKeyboard.Open(note, TouchScreenKeyboardType.Default);
        cloudBehaviour.CloudText.text = note;
        cloudBehaviour.CloudText.gameObject.SetActive(true);

        //when player is finshed inputing note
        if(keyboard.status == TouchScreenKeyboard.Status.Done
            || keyboard.status == TouchScreenKeyboard.Status.Canceled 
            || keyboard.status == TouchScreenKeyboard.Status.LostFocus)
        {
            //move cloud back to the sky 
            paused = false;
            SettingsMenue.setCanBeOpened(false);
        }
    }


    private void initHelpLineNumber()
    {
        paused = true;
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
            paused = false;
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
        if (paused)
        {
            Time.timeScale = 1;
           // paused = false;
        }
        else
        {
            Time.timeScale = 0;
           // paused = true;
        }
    }

    public void setPaused(bool P)// need to set settings button to unrepsonive during paused
    {
        paused = true;
    }
    public bool getPaused()
    {
        return paused;
    }


    public void Quit()
    {
        Application.Quit();
    }
    
}
