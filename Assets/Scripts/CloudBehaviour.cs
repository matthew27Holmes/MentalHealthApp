using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudBehaviour : MonoBehaviour {

    float speed = 5.0f;
    bool dead = false;
    bool ParticlesTriggered = false;

    public Text CloudText;
    GameManger GM;

    bool moveCloud;
    bool movingBack;
    Vector3 CloudStartPos;
    Vector3 moveToPostion;

    // Use this for initialization
    void Start () {
        
        dead = false;
        movingBack = false;
        moveCloud = false;
        ParticlesTriggered = false;

        GM = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();

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

        if(moveCloud)
        {
            moveCloudToPostion();
            float distanceToEnd = Vector3.Distance(transform.position, moveToPostion);
            if (distanceToEnd <= 1)
            {
                moveCloud = false;
                if (!movingBack)
                {
                    GM.LeaveCloudMessage(this);
                }
                else
                {
                    movingBack = false;
                    GM.setPaused(false);
                }
            }
        }

        if (dead)
        {
            DestoryCloud();
        }
    }

    public void setCloudMove(Vector3 start,Vector3 end)
    {
        moveCloud = true;
        movingBack = false;
        CloudStartPos = start;
        moveToPostion = end;
    }

    public void moveCloudBackToStart()
    {
        moveCloud = true;
        movingBack = true;
        moveToPostion = CloudStartPos;
    }

    void moveCloudToPostion()
    {
        // lerp to move to pos
        float speed = 0.5f;
        transform.position = Vector3.MoveTowards(transform.position, moveToPostion, speed);// Time.deltaTime
    }

    public void setCloudText(string note)
    {
        CloudText.gameObject.SetActive(true);
        CloudText.text = note;
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
                Destroy(CloudText.gameObject);
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
