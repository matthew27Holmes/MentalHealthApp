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

    public void LeaveCloudMessage(GameObject cloud)
    {
        //change cloud postion to center stage 
        //bring up text box overlay 
        // create text 
        // assaign text with text overlay 
        // make text a child of cloud
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
