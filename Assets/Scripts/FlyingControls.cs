using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
[ExecuteInEditMode]

public class FlyingControls : MonoBehaviour
{
    #region mouse Control variables
        Vector2 firstPressPos;
        Vector2 secondPressPos;
        Vector2 currentSwipe;
    #endregion

    #region SwipeVariables

        Vector3 fp;   //First touch position
        Vector3 lp;   //Last touch position
        float dragDistance;  //minimum distance for a swipe to be registered

    float touchTimeStart, touchTimeFinish, timeInterval; // to calculate swipe time to sontrol throw force in Z direction

        [SerializeField]
        float throwForceInXandY = 1f; // to control throw force in X and Y directions

        [SerializeField]
        float throwForceInZ = 50f; // to control throw force in Z direction

        Rigidbody rb;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dragDistance = Screen.height * 5 / 100; //dragDistance is 15% height of the screen
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        MouseSwipe();
#endif
#if UNITY_ANDROID
        SwipeControls();
#endif
    }

    public void SwipeControls()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchTimeStart = Time.time;

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
                    // marking time when you release it
                    touchTimeFinish = Time.time;

                    // calculate swipe time interval 
                    timeInterval = touchTimeFinish - touchTimeStart;

                    // calculating swipe direction in 2D space
                    Vector2 direction = fp - lp;

                    // add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
                    rb.AddForce(-direction.x * throwForceInXandY, -direction.y * throwForceInXandY, throwForceInZ / timeInterval);
                }
            }
        }
    }

    public void MouseSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchTimeStart = Time.time;

            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);            
        }
        if (Input.GetMouseButtonUp(0))
        {

            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = secondPressPos - firstPressPos;

            //currentSwipe.Normalize();

            // marking time when you release it
            touchTimeFinish = Time.time;

            // calculate swipe time interval 
            timeInterval = touchTimeFinish - touchTimeStart;

            // add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
            rb.AddForce( currentSwipe.x * throwForceInXandY, currentSwipe.y * throwForceInXandY, throwForceInZ / timeInterval);

        }
    }
    
}

