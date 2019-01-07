using UnityEngine;
using UnityEngine.SceneManagement;

public class playerControler : MonoBehaviour {

    Vector3 targetPos;
    public GameObject swipeInput;

    public float ZMod;
    public float speed;
    public float initialAngle;
    public float MaxH, MinH;

    Vector3 fp;   //First touch position
    Vector3 lp;   //Last touch position
    float dragDistance;  //minimum distance for a swipe to be registered

    public bool OnPlatform;
    public Vector3 jumpToPostion;
    public int playersLane;
    public string[] LaneTags;
    

    void Start()
    {
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
        OnPlatform = true;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveLeft();
        }
         if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveRight();
        }
        findPlayersLane();

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        SwipeControls();
    }

    void findPlayersLane()
    {
        if(transform.position.z > 0)
        {
            playersLane = 0;
        }
        else if(transform.position.z < 0 )
        {
            playersLane = 2;
        }
        else
        {
            playersLane = 1;
        }
    }

    public void SwipeControls()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
                swipeInput.transform.position = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
                swipeInput.transform.position = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% if less then = tap
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {
                    //check if the drag is vertical or horizontal
                    //if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    //{   //If the horizontal movement is greater than the vertical movement...
                    if ((lp.x > fp.x))
                    {
                        Debug.Log("Right Swipe");
                        moveRight();
                    }
                    else
                    {
                        Debug.Log("Left Swipe");
                        moveLeft();
                    }
                    //the vertical movement is greater than the horizontal movement
                    if (lp.y > fp.y)
                    {
                        Debug.Log("Up Swipe");
                        Jump();
                    }
                    //}
                    //else
                    //{   //the vertical movement is greater than the horizontal movement
                    //    if (lp.y > fp.y)
                    //    {   
                    //        Debug.Log("Up Swipe");
                    //        Jump();
                    //    }
                    //}
                }
            }
        }
    }

    public void moveLeft()
    {
        if (transform.position.z < MaxH)
        {
            targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + ZMod);

            //transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            transform.position = targetPos;
        }
    }

    public void moveRight()
    {
        if (transform.position.z > MinH)
        {
            targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - ZMod);

            // transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            transform.position = targetPos;
        }
    }

    Vector3 FindNextPlatform()
    {
        Transform NearestPlat = null;
        float minDist = Mathf.Infinity;

        GameObject[] PlatformsInLane;
        PlatformsInLane = GameObject.FindGameObjectsWithTag(LaneTags[playersLane]);
        foreach (GameObject  platform in PlatformsInLane)
        {
            float dis = transform.position.x - platform.transform.position.x;
            if(dis < minDist)
            {
                NearestPlat = platform.transform;
            }
        }

        // check plat form is in range 

        float MaxRange = 50.0f;
        if (NearestPlat == null || transform.position.x - NearestPlat.position.x > MaxRange)
        {
            Debug.Log("out of range");
            float InrangeX = transform.position.x + MaxRange;
            return new Vector3(InrangeX, transform.position.y, transform.position.z);
        }

        // account for platform speed
        Vector3 NearestPlatPostion = NearestPlat.position;
        if (OnPlatform)
        {
            ObsticelBehaviour PlatformBehaviour = NearestPlat.gameObject.GetComponent<ObsticelBehaviour>();
            NearestPlatPostion.x -= 30.0f * PlatformBehaviour.speed ; // fudge * (obsticle speed * Time.deltaTime);
        }
        jumpToPostion = NearestPlatPostion;
        return NearestPlatPostion;
    }


    public void Jump()
    {
        //float upForce = 20, forwardForce = 5;

        //Rigidbody Rb = GetComponent<Rigidbody>();

        //Rb.AddForce(Vector3.up * upForce);
        //Rb.AddForce(Vector3.right * forwardForce);

        Vector3 p = FindNextPlatform();

       Rigidbody rigid = GetComponent<Rigidbody>();

        float gravity = Physics.gravity.magnitude;

        // Selected angle in radians
        float angle = initialAngle * Mathf.Deg2Rad;

        // Positions of this object and the target on the same plane
        Vector3 planarTarget = new Vector3(p.x, 0, p.z);
        Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

        // Planar distance between objects
        float distance = Vector3.Distance(planarTarget, planarPostion);
        // Distance along the y axis between objects
        float yOffset = transform.position.y - p.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        // Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        // Fire!
        rigid.velocity = finalVelocity;
    }

    public void die()
    {
        Debug.Log("blah i died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "StartingPlatform")
        {
            OnPlatform = false;
        }
    }
}
