﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {

    public AudioClip song1;
    public AudioClip song2;
    public AudioClip CloudClips;


    public AudioClip flyingSound;
    AudioSource ASource;
    public AudioSource EnviromentSound;

    public GameManger GM;


    public GameObject birdModel;
    Animator anim;

    //hash variables for the animation states and animation properties
    int flyingBoolHash;
    int flyingDirectionXHash;
    int flyingDirectionYHash;

    float dragDistance;  //minimum distance for a swipe to be registered


    Dictionary<int, Vector2> activeTouches = new Dictionary<int, Vector2>();
    private bool TapTouch;

    CharacterController controller;
    public float baseSpeed = 10.0f;
    public float rotSpeedX = 3.0f;
    public float rotSpeedY = 1.5f;

    public bool UTurn = false;


    private void Start()
    {
        controller = GetComponent<CharacterController>();

        ASource = birdModel.GetComponent<AudioSource>();

        anim = birdModel.GetComponent<Animator>();
        flyingBoolHash = Animator.StringToHash("flying");
        flyingDirectionXHash = Animator.StringToHash("flyingDirectionX");
        flyingDirectionYHash = Animator.StringToHash("flyingDirectionY");

        dragDistance = Screen.height * 0.001f; //dragDistance is % height of the screen
    }

    private void FixedUpdate()
    {
        anim.SetBool(flyingBoolHash, true);
        ASource.PlayOneShot(flyingSound, .1f);

        Vector3 inputs = GetPlayerSwipe();


        if (!UTurn)
        {
            Fly(inputs);
        }
        else
        {
            UTurnBehaviour();
        }
        
    }


    void PlaySong()
    {
        if (Random.value < .5)
        {
            ASource.PlayOneShot(song1, 1);
        }
        else
        {
            ASource.PlayOneShot(song2, 1);
        }
    }

    void Fly(Vector3 inputs)
    {
        Vector3 moveVector = transform.forward * baseSpeed;

        Vector3 yaw = inputs.x * transform.right * rotSpeedX * Time.deltaTime;
        anim.SetFloat(flyingDirectionXHash, inputs.x * rotSpeedX);

        Vector3 pitch = inputs.y * transform.up * rotSpeedY * Time.deltaTime;
        anim.SetFloat(flyingDirectionYHash, inputs.y * rotSpeedY);

        Vector3 direction = yaw + pitch;
       

        // stop loops 
        if(StopLoop(moveVector + direction))
        {
            moveVector += direction;
            transform.rotation = Quaternion.LookRotation(moveVector);
        }

        controller.Move(moveVector * Time.deltaTime);
    }

    Vector3 GetPlayerSwipe()
    {
        Vector3 Swipe = Vector3.zero;

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                activeTouches.Add(touch.fingerId, touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (activeTouches.ContainsKey(touch.fingerId))
                {
                    activeTouches.Remove(touch.fingerId);
                }
            }
            else
            {
                float mag = 0;
                Swipe = (touch.position - activeTouches[touch.fingerId]);
                if (Mathf.Abs(Swipe.x) > dragDistance || Mathf.Abs(Swipe.y) > dragDistance || Mathf.Abs(Swipe.z) > dragDistance)
                {
                    mag = Swipe.magnitude / 300;
                    Swipe = Swipe.normalized * mag;
                }
                else
                {

                    // tap not drag play sound
                    if (!TapTouch)
                    {
                        TapTouch = true;
                        TapRay(touch.position);
                    }
                }
            }
        }
        return Swipe;
    }

    void TapRay(Vector3 TapPos)
    {
        RaycastHit hit;
        Camera cam = transform.GetComponentInChildren<Camera>();
        if (Physics.Raycast(TapPos, cam.transform.forward, out hit, LayerMask.NameToLayer("Cloud")))
        {
            Debug.Log("Ray Hit Cloud");
            GameObject cloud = hit.transform.gameObject;

            GM.LeaveCloudMessage(cloud);
        }
        TapTouch = false;
    }

    //need to handel loops better
    bool StopLoop(Vector3 moveVector)
    {
        float maxXRotaion = Quaternion.LookRotation(moveVector).eulerAngles.x;
       // float maxYRotaion = Quaternion.LookRotation(moveVector).eulerAngles.y;
        //|| (maxYRotaion < 90 && maxYRotaion > 70 || maxYRotaion > 270 && maxYRotaion < 290))
        if (maxXRotaion < 90 && maxXRotaion > 70 || maxXRotaion > 270 && maxXRotaion < 290)             
        {
           // Debug.Log("flip");
            return false;
        }
        else
        {
            return true;
        }

    }

    void UTurnBehaviour()
    {
        Vector3 moveVector = transform.forward * (baseSpeed * 2);

        Vector2 Inverse = -transform.position.normalized;//new Vector2(0.5f,0.5f);

        Vector3 yaw = Inverse.x * transform.right * (rotSpeedX * 2) * Time.deltaTime;
        anim.SetFloat(flyingDirectionXHash, Inverse.x);

        Vector3 pitch = Inverse.y * transform.up * (rotSpeedY *2) * Time.deltaTime;
        anim.SetFloat(flyingDirectionYHash, Inverse.y);

        Vector3 direction = yaw + pitch;
        moveVector += direction;

        if (StopLoop(moveVector + direction))
        {
            moveVector += direction;
            transform.rotation = Quaternion.LookRotation(moveVector);
        }

        controller.Move(moveVector * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Cloud"))
        {
            // AudioClip clip = //CloudClips[Random.Range(0, CloudClips.Length)];
            if (!EnviromentSound.isPlaying)
            {
                Debug.Log("playing " + CloudClips.name);
                EnviromentSound.PlayOneShot(CloudClips, 1);
            }
        }
    }
}


