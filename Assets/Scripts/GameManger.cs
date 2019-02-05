using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour {

    private static bool GameManagerExists;
   // private static string currentScene;
    public bool paused = false;
    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

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
        
    }

    public void changeScene(string sceneToChangeTo)
    {
       // currentScene = sceneToChangeTo;
        SceneManager.LoadScene(sceneToChangeTo);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
