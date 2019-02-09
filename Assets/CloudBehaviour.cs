using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour {

    public float speed = 1.0f;

	// Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // Move the object upward in world space 1 unit/second.
        transform.Translate(Vector3.right * Time.deltaTime, Space.World);

    }

    private void DestoryCloud()
    {
        // strat particle effect kill cloud
        // destroy clouds


       
    }
}
