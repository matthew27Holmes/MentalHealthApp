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

    public GameManger GM;
    bool CanBeOpened = true;
    bool open = false;

    //https://stackoverflow.com/questions/48906129/make-phone-call-in-unity?noredirect=1&lq=1
    public void CallHelpLine()
    {

        /*should look at aoutdailing*/
        //take in any personal numbers GPs/Friends
        if (PlayerPrefs.GetInt("helpLineGiven") == 1)
        {
            string phoneNumber = PlayerPrefs.GetString(GM.helpLineKey);
            Application.OpenURL(phoneNumber);
        }  
    }

    public bool getCanBeOpened()
    {
        return CanBeOpened;
    }
    public void setCanBeOpened(bool t)
    {
        CanBeOpened = t;
    }

    public void MenueButtonPressed()
    {
        if (CanBeOpened)
        {
            if (GM.getPaused())
            {
                OpenCloseMenue(false);
                GM.setPaused(false);
            }
            else
            {
                OpenCloseMenue(true);
                GM.setPaused(true);
            }
        }
    }

    private void OpenCloseMenue(bool ismenueOpen)
    {
        for(int i =0; i <= transform.childCount;i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(ismenueOpen);
        }
    }

    public void UpdateVolumeMusic()
    {

    }
    public void UpdateVoloumeFX()
    {

    }
}
