using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarderPushBack : MonoBehaviour {

    float BreezeFroce = 0.5f;

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {

        }
    }

    void pushBackBreeze(GameObject player)
    {
        Rigidbody rigidbody = player.GetComponent<Rigidbody>();
        Vector3 PushBackVelocity = transform.InverseTransformDirection(rigidbody.velocity);

        rigidbody.AddForce(PushBackVelocity * BreezeFroce);
    }
}
