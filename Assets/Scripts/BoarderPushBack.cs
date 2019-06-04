using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarderPushBack : MonoBehaviour
{
    public bool KeepWithinSystem;
    AudioSource PushBackSound;
    public AudioClip windClip;
    private void Start()
    {
        PushBackSound = GetComponent<AudioSource>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (KeepWithinSystem)
            {
                BirdController bird = other.gameObject.GetComponent<BirdController>();              
                PushBackSound.PlayOneShot(windClip, 1);
                bird.UTurn = true;
            }
            else
            {
                BirdController bird = other.gameObject.GetComponent<BirdController>();
                bird.UTurn = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (KeepWithinSystem)
            {
                BirdController bird = other.gameObject.GetComponent<BirdController>();
                bird.UTurn = false;
            }
            else
            {
                BirdController bird = other.gameObject.GetComponent<BirdController>();
                bird.UTurn = true;
                PushBackSound.PlayOneShot(windClip, 1);
            }
        }
    }
}


