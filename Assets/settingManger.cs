using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingManger : MonoBehaviour {


    /*
     should be able to set music volume 
     sound effect vloume 
     link to help line 
     wiget the brings up phone */
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //https://stackoverflow.com/questions/48906129/make-phone-call-in-unity?noredirect=1&lq=1
    public void CallHelpLine()
    {
        string phoneNum = "tel: +79011111115";

        //For accessing static strings(ACTION_CALL) from android.content.Intent
        AndroidJavaClass intentStaticClass = new AndroidJavaClass("android.content.Intent");
        string actionCall = intentStaticClass.GetStatic<string>("ACTION_CALL");

        //Create Uri
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", phoneNum);

        //Pass ACTION_CALL and Uri.parse to the intent
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", actionCall, uriObject);

        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");


        //Start Activity
        unityActivity.Call("startActivity", intent);

    }

    public void UpdateVolumeMusic()
    {

    }
    public void UpdateVoloumeFX()
    {

    }
}
