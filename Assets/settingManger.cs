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

    string HelpLine = "tel: 01752791173";

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //https://stackoverflow.com/questions/48906129/make-phone-call-in-unity?noredirect=1&lq=1
    public void CallHelpLine()
    {

        /*should look at aoutdailing*/
        //take in any personal numbers GPs/Friends
        Application.OpenURL(HelpLine);

    }

    public void UpdateVolumeMusic()
    {

    }
    public void UpdateVoloumeFX()
    {

    }
}
