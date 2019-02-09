using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarderPushBack : MonoBehaviour
{

    //  float BreezeFroce = 0.5f;

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        BirdController bird = other.gameObject.GetComponent<BirdController>();
    //        bird.UTurn = true;
    //    }
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        BirdController bird = other.gameObject.GetComponent<BirdController>();
    //        bird.UTurn = false;
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            BirdController bird = other.gameObject.GetComponent<BirdController>();
            bird.UTurn = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            BirdController bird = other.gameObject.GetComponent<BirdController>();
            bird.UTurn = true;
        }
    }
}


