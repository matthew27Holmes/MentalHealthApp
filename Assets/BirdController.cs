using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {

    public AudioClip song1;
    public AudioClip song2;
    public AudioClip flyAway1;
    public AudioClip flyAway2;
    AudioSource ASource;

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


    private void Start()
    {
        controller = GetComponent<CharacterController>();

        ASource = birdModel.GetComponent<AudioSource>();

        anim = birdModel.GetComponent<Animator>();
        flyingBoolHash = Animator.StringToHash("flying");
        flyingDirectionXHash = Animator.StringToHash("flyingDirectionX");
        flyingDirectionYHash = Animator.StringToHash("flyingDirectionY");
    }

    private void Update()
    {
        Fly();
        //PlaySong();
        //Gravity();
    }


    void PlaySong()
    {
        if (Random.value < .01)
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

    }

    void Fly()
    {

        if (Random.value < .5)
        {
            ASource.PlayOneShot(flyAway1, .1f);
        }
        else
        {
            ASource.PlayOneShot(flyAway2, .1f);
        }

        anim.SetBool(flyingBoolHash, true);

        Vector3 moveVector = transform.forward * baseSpeed;

        Vector3 inputs = GetPlayerSwipe();

        Vector3 yaw = inputs.x * transform.right * rotSpeedX * Time.deltaTime;
        anim.SetFloat(flyingDirectionXHash, inputs.x);

        Vector3 pitch = inputs.y * transform.up * rotSpeedY * Time.deltaTime;
        anim.SetFloat(flyingDirectionYHash, inputs.y);

        Vector3 direction = yaw + pitch;
        moveVector += direction;

        transform.rotation = Quaternion.LookRotation(moveVector);

        controller.Move(moveVector * Time.deltaTime);

        //anim.SetFloat(flyingDirectionHash, 0);
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
                mag = Swipe.magnitude / 300;
                Swipe = Swipe.normalized * mag;
            }
        }
        return Swipe;
    }

   
    //Sets a variable between -1 and 1 to control the left and right banking animation
    float FindBankingAngle(Vector3 birdForward, Vector3 dirToTarget)
    {
        Vector3 cr = Vector3.Cross(birdForward, dirToTarget);
        float ang = Vector3.Dot(cr, Vector3.up);
        return ang;
    }


    void Gravity()
    {

    }
}


