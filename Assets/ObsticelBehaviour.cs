using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticelBehaviour : MonoBehaviour {

    public float speed = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Move the object forward along its z axis 1 unit/second.
        transform.Translate(Vector3.back * (speed * Time.deltaTime));
    }
}
