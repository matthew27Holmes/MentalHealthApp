using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarderPushBack : MonoBehaviour {

    float BreezeFroce = 0.5f;

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            //pushBackBreeze(other.gameObject);
        }
    }


    /// <summary>
    /// Get players direction 
    /// slowly turn around so they are facing the way they came from 
    /// </summary>
    /// <param name="player"></param>
    void pushBackBreeze(GameObject player)
    {
        // should flip player upside down and push back 

        Rigidbody rigidbody = player.GetComponent<Rigidbody>();
        Vector3 PushBackVelocity = transform.InverseTransformDirection(rigidbody.velocity);

        rigidbody.AddForce(PushBackVelocity * BreezeFroce);
    }
}
