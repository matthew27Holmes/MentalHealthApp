using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinStateHandler : MonoBehaviour {

    public GameManger GM;
    public AudioSource Audio;

	void Update () {
		if(!Audio.isPlaying && !GM.paused)
        {
            //GM.StopGame();
            //childrenActiveState(true);
            //setSongTitle();
        }
	}
}
