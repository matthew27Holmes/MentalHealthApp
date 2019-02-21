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

    private void Start()
    {
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

    public string InputText()
    {
        string inputText = "";
        TouchScreenKeyboard keyboard = TouchScreenKeyboard.Open(inputText, TouchScreenKeyboardType.Default);
        return inputText;
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
