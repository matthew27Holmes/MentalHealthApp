using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFlickFlying : MonoBehaviour {

    Dictionary<int, Vector2> activeTouches = new Dictionary<int, Vector2>();

    CharacterController controller;
    float baseSpeed = 10.0f;
    float rotSpeedX = 3.0f;
    float rotSpeedY = 1.5f;


    private void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    private void Update()
    {
        Vector3 moveVector = transform.forward * baseSpeed;

        Vector3 inputs = GetPlayerSwipe();

        Vector3 yaw = inputs.x * transform.right * rotSpeedX * Time.deltaTime;
        Vector3 pitch = inputs.y * transform.up * rotSpeedY * Time.deltaTime;
        Vector3 direction = yaw + pitch;

        moveVector += direction;
        transform.rotation = Quaternion.LookRotation(moveVector);

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
                mag = Swipe.magnitude / 300;
                Swipe = Swipe.normalized * mag;
            }
        }
        return Swipe;
    }
}

