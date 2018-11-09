using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Analytics : MonoBehaviour {
    int sceneChangeNum = 0;
    Text m_Text;

    // Use this for initialization
    void Awake () {
        updateSceneChange();
        m_Text = this.GetComponent<Text>();
        setText();
    }

	void updateSceneChange()
    {
        sceneChangeNum = PlayerPrefs.GetInt("sceneChangeNum");
        sceneChangeNum++;
        PlayerPrefs.SetInt("sceneChangeNum", sceneChangeNum);
        PlayerPrefs.Save();
    }

    void setText()
    {
        m_Text.text = "you have come to this Scene" + sceneChangeNum + "many times";
    }
	
}
