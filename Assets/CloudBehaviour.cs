using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour {

    float speed = 5.0f;
  //  AudioProcessor BeatDetc;

	// Use this for initialization
    void Start () {
        //GameObject enviroment = GameObject.FindWithTag("terrianManger");
        //BeatDetc = enviroment.GetComponent<AudioProcessor>();

    }

    public void setSpeed(float nwSpeed)
    {
        speed = nwSpeed;
    }
	// Update is called once per frame
	void Update () {

       // speed = BeatDetc.tapTempo();
        // Move the object upward in world space 1 unit/second.
        transform.Translate(Vector3.right * (speed * Time.deltaTime),Space.World);

    }

    private void DestoryCloud()
    {
        // strat particle effect kill cloud
        // destroy clouds// should look at object pooling

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            DestoryCloud();
        }
    }
}
