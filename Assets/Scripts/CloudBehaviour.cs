using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudBehaviour : MonoBehaviour {

    float speed = 5.0f;
    bool dead = false;
    bool ParticlesTriggered = false;

    public Text CloudText;

    // Use this for initialization
    void Start () {
        
        dead = false;
        ParticlesTriggered = false;

        GameObject canvas = GameObject.FindGameObjectWithTag("CloudTextManger");
        CloudText = Instantiate(CloudText.gameObject, this.transform.position,Quaternion.identity).GetComponent<Text>();
        
        CloudText.gameObject.SetActive(false);
        CloudText.name = this.name;
        CloudText.transform.SetParent(canvas.transform,false);
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

        //UpdateTextPostion
        Vector3 textPos = Camera.main.WorldToScreenPoint(this.transform.position);//should use anchor  
        CloudText.transform.position = textPos;

        if (dead)
        {
            DestoryCloud();
        }

    }
    
    public void LeaveCloudMessage(string note)
    {
        CloudText.text = note;
        CloudText.gameObject.SetActive(true);
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
                Destroy(CloudText);
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
