using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {

    public AudioClip[] CloudClips;
    public AudioClip[] FlowerClips;
    public AudioClip[] LillyFlowerClips;
    public AudioClip[] TreeClips;


    public AudioSource EnviromentSound;

    public GameManger GM;
    public Camera cam;


    public GameObject birdModel;
    Animator anim;

    //hash variables for the animation states and animation properties
    int flyingBoolHash;
    int flyingDirectionXHash;
    int flyingDirectionYHash;

    Dictionary<int, Vector2> activeTouches = new Dictionary<int, Vector2>();

    CharacterController controller;
    public float baseSpeed = 10.0f;
    public float rotSpeedX = 3.0f;
    public float rotSpeedY = 1.5f;
    public Vector3 pitch;
    public Vector3 yaw;
    private Vector3 direction;

    private Vector3 LastHeading;
    private float startTime;

    public bool UTurn = false;


    private void Start()
    {
        controller = GetComponent<CharacterController>();

        anim = birdModel.GetComponent<Animator>();
        flyingBoolHash = Animator.StringToHash("flying");
        flyingDirectionXHash = Animator.StringToHash("flyingDirectionX");
        flyingDirectionYHash = Animator.StringToHash("flyingDirectionY");

        LastHeading = new Vector3();
    }

    private void FixedUpdate()
    {
        anim.SetBool(flyingBoolHash, true);
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

    void Fly(Vector3 inputs)
    {
        Vector3 moveVector = transform.forward * baseSpeed;

        yaw = inputs.x * transform.right * rotSpeedX * Time.deltaTime;

        pitch = inputs.y * transform.up * rotSpeedY * Time.deltaTime;

        direction = yaw + pitch;
       
        // stop loops 
        if(StopLoop(moveVector + direction))
        {
            moveVector += direction;
            transform.rotation = Quaternion.LookRotation(moveVector);
        }

        //lession the snap back to default postion
        if (inputs.x <= 0 && inputs.y <= 0)
        {
            float speedFudge = 1.0f;
            float DistanceCovered = (Time.time - startTime) * speedFudge;
            float JourneyLength = Vector3.Distance(inputs, LastHeading);
            float fractJourney = DistanceCovered / JourneyLength;

            Vector3 heading = Vector3.Lerp(LastHeading, inputs, fractJourney);

            // Vector3 heading = Vector3.SmoothDamp(LastHeading, inputs,ref velocity, speedFudge,Time.deltaTime);
            anim.SetFloat(flyingDirectionXHash, heading.x /** rotSpeedX*/);
            anim.SetFloat(flyingDirectionYHash, heading.y /** rotSpeedY*/);
        }
        else
        {
            LastHeading = inputs;
            startTime = Time.time;
            anim.SetFloat(flyingDirectionXHash, inputs.x /** rotSpeedX*/);//took out rotaion for sensitiveity issue
            anim.SetFloat(flyingDirectionYHash, inputs.y /** rotSpeedY*/);
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
                if (!activeTouches.ContainsKey(touch.fingerId))
                {
                    activeTouches.Add(touch.fingerId, touch.position);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (activeTouches.ContainsKey(touch.fingerId))
                {
                    if (Vector2.Distance(touch.position, activeTouches[touch.fingerId]) <= 1)
                    {
                        TapRay(cam.ScreenToWorldPoint(touch.position));
                    }
                    activeTouches.Remove(touch.fingerId);
                }
            }
            else
            {
                if (activeTouches.ContainsKey(touch.fingerId))
                {
                    float mag = 0;
                    Swipe = (touch.position - activeTouches[touch.fingerId]);

                    mag = Swipe.magnitude / 300;
                    Swipe = Swipe.normalized * mag;
                }
            }
        }
        return Swipe;
    }

    void TapRay(Vector3 TapPos)
    {
        RaycastHit hit;
       
        Debug.DrawRay(TapPos, transform.forward * 100,Color.green,10);
        Debug.Log("Ray cast");
        if (Physics.Raycast(TapPos, cam.transform.forward, out hit)) //  Vector3 direction

        {
            Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Cloud"))
            {
                Debug.Log("Ray Hit Cloud");
                GameObject cloud = hit.transform.gameObject;

                GM.moveCloudToPostion(cloud, transform.position);
            }
        }
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

    void FindObjectSound(string type)
    {
        AudioClip clip = null;
        bool souceFound = false;
        float voulmeScale = 1;
        //Debug.Log(type.ToString());
        switch (type)
        {
            case "LillyFlower":
                clip = LillyFlowerClips[Random.Range(0, LillyFlowerClips.Length)];//dont make this random
                souceFound = true;
                break;
            case "Flower":
                clip = FlowerClips[Random.Range(0, FlowerClips.Length)];
                souceFound = true;
                break;
            case "Cloud":
                clip = CloudClips[Random.Range(0, CloudClips.Length)];
                voulmeScale = 3;
                souceFound = true;
                break;          
            case "OldTree":
                clip = TreeClips[Random.Range(0, TreeClips.Length)];
                souceFound = true;
                break;
            default:
                break;
        }

        if (souceFound)//!EnviromentSound.isPlaying && 
        {
            EnviromentSound.PlayOneShot(clip, voulmeScale);
            Debug.Log("playing " + clip.name);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        FindObjectSound(LayerMask.LayerToName(other.gameObject.layer));
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.layer == LayerMask.NameToLayer("Cloud"))
    //    {
    //        // AudioClip clip = //CloudClips[Random.Range(0, CloudClips.Length)];
    //        if (!EnviromentSound.isPlaying)
    //        {
    //            Debug.Log("playing " + CloudClips.name);
    //            EnviromentSound.PlayOneShot(CloudClips, 1);
    //        }
    //    }
    //}
}


