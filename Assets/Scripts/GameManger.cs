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
    private string note;

    private CloudBehaviour currentClickedCloud;

    private void Start()
    {
        paused = false;
        note = "";
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

    public void Update()
    {
        Pause();

        if (currentClickedCloud != null)
        {
            if (!keyboard.active)
            {
                note = keyboard.text;
                currentClickedCloud.setCloudText(note);
                Debug.Log("cloud text set: " + note);
                currentClickedCloud.moveCloudBackToStart();              
            }

            if(!paused)
            {
                SettingsMenue.setCanBeOpened(true);//this needs to wait till cloud is in postion again
                note = "";
                currentClickedCloud = null;
            }
        }
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
        cloudBehaviour.setCloudMove(cloud.transform.position, playerPos);
    }

    public void LeaveCloudMessage(CloudBehaviour cloudBehaviour)
    {
        currentClickedCloud = cloudBehaviour;
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
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
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;//change slowly for cloud write
        }
    }

    public void setPaused(bool P)// need to set settings button to unrepsonive during paused
    {
        paused = P;
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
