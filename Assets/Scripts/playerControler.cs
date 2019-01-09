using UnityEngine;
using UnityEngine.SceneManagement;

public class playerControler : MonoBehaviour {

    Vector3 targetPos;
    public GameObject swipeInput;
    public Camera mainCamera;

    public float ZMod;
    public float speed;
    public float initialAngle;
    public float MaxH, MinH;

    Vector3 fp;   //First touch position
    Vector3 lp;   //Last touch position
    float dragDistance;  //minimum distance for a swipe to be registered

    //mouse debug control 
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public bool OnPlatform;
    public Vector3 jumpToPostion;
    public int playersLane;
    public string[] LaneTags;
    public bool inAir;
    

    void Start()
    {
        dragDistance = Screen.height * 5 / 100; //dragDistance is 15% height of the screen
        OnPlatform = true;
        playersLane = 1;
    }

    // Update is called once per frame
    void Update () {

        createTouchTrail();

        if (!inAir)
        {
            MouseSwipe();
            SwipeControls();
        }
    }

    void createTouchTrail()
    {
        float Distance = 10;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 pos = r.GetPoint(Distance);
        swipeInput.transform.position = pos;
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
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position; 

                //Check if drag distance is greater than 20% if less then = tap
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {
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
                    if (lp.y > fp.y)
                    {
                        Debug.Log("Up Swipe");
                        inAir = true;
                        Jump();
                    }
                }
            }
        }
    }

    public void MouseSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            if (currentSwipe.x < 0)
            {
                moveLeft();
            }else if (currentSwipe.x > 0)
            {
                moveRight();
            }

            if (currentSwipe.y > 0)
            {
                inAir = true;
                Jump();
            }
          
        }
    }

    public void moveLeft()
    {
        if (playersLane > 0)
        {
            playersLane -= 1;
        }     
    }

    public void moveRight()
    {
        if (playersLane < 2)
        {
            playersLane += 1;
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

        float MaxRange = 20.0f;
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
            NearestPlatPostion.x -= 2.0f * PlatformBehaviour.speed ; // fudge * (obsticle speed * Time.deltaTime);
        }
        jumpToPostion = NearestPlatPostion;
        return NearestPlatPostion;
    }


    public void Jump()
    {
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {
            inAir = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "StartingPlatform")
        {
            OnPlatform = false;
        }  
    }
}
