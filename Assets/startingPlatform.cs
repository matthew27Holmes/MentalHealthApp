using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startingPlatform : MonoBehaviour {

    public float speed;// same as obsticle speed
    bool Move = false;


    public void Update()
    {
     if(Move)
        {
            transform.Translate(Vector2.left * (speed * Time.deltaTime));
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Move = true;
        }
    }
}
