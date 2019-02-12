using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour {

    float speed = 5.0f;
    bool dead = false;
    bool ParticlesTriggered = false;
  //  AudioProcessor BeatDetc;

	// Use this for initialization
    void Start () {
        //GameObject enviroment = GameObject.FindWithTag("terrianManger");
        //BeatDetc = enviroment.GetComponent<AudioProcessor>();
        dead = false;
        ParticlesTriggered = false;
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
        if(dead)
        {
            DestoryCloud();
        }

    }

    private void DestoryCloud()
    {
        // strat particle effect kill cloud
        // destroy clouds// should look at object pooling
        ParticleSystem particle = GetComponent<ParticleSystem>();

        if (!ParticlesTriggered)
        {
           
                particle.Play();
                MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
                meshRenderer.enabled = false;
            ParticlesTriggered = true;
        }
        else
        {
            if (!particle.IsAlive())
            {
                Destroy(this.gameObject);
            }
        }
      
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            dead = true;
        }
    }
}
