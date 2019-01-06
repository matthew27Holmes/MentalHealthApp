using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManger : MonoBehaviour
{

    public Sprite Pause;
    public Sprite Play;
    public GameManger GM;
    private Image currentImage;
    public AudioSource Audio;

    public void Start()
    {
        GetComponent<Image>().sprite = Pause;
        childrenActiveState(false);
    }

    public void childrenActiveState(bool ActiveState)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(ActiveState);
        }
    }

    public void setSongTitle()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "SongText")
            {
                Text text = child.transform.GetChild(0).gameObject.GetComponent<Text>();
                text.text = Audio.clip.name;
            }
        }
    }

    void Update()
    {
        if (GM.paused)
        {
            GetComponent<Image>().sprite = Play;
            childrenActiveState(true);
            setSongTitle();
        }
        else
        {
            GetComponent<Image>().sprite = Pause;
            childrenActiveState(false);
        }
    }
}
