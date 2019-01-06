using UnityEngine;
using UnityEngine.SceneManagement;

public class playerControler : MonoBehaviour {

    private Vector3 targetPos;
    public GameObject swipeInput;

    public float ZMod;
    public float speed;
    public float MaxH, MinH;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    public bool OnPlatform;

    void Start()
    {
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
        OnPlatform = true;
    }

    // Update is called once per frame
    void Update () {

        if(Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveLeft();
        }
         if (Input.GetKey(KeyCode.RightArrow))
        {
            moveRight();
        }
            if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
                swipeInput.transform.position = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
                swipeInput.transform.position = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            moveRight();
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            moveLeft();
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                            Jump();
                        }
                        //else
                        //{   //Down swipe
                        //    Debug.Log("Down Swipe");
                        //}
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }

    public void moveLeft()
    {
        if (transform.position.z < MaxH)
        {
            targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + ZMod);

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }

    public void moveRight()
    {
        if (transform.position.z > MinH)
        {
            targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - ZMod);

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }

    public void Jump()
    {
        float upForce = 20, forwardForce = 5;

        Rigidbody Rb = GetComponent<Rigidbody>();

        Rb.AddForce(Vector3.up * upForce);
        Rb.AddForce(Vector3.right * forwardForce);
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
