using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
[ExecuteInEditMode]

public class FlyingControls : MonoBehaviour
{
    Vector3 targetPos;
    public GameObject swipeInput;
    public Camera mainCamera;


    Vector3 fp;   //First touch position
    Vector3 lp;   //Last touch position
    float dragDistance;  //minimum distance for a swipe to be registered

    //mouse debug control 
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;



    void Start()
    {
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
                    }
                    else
                    {
                        Debug.Log("Left Swipe");
                    }
                    if (lp.y > fp.y)
                    {
                        Debug.Log("Up Swipe");

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
            }
            else if (currentSwipe.x > 0)
            {
            }

            if (currentSwipe.y > 0)
            {

            }
        }
    }
    
}

