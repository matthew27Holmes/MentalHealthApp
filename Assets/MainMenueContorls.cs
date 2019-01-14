using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenueContorls : MonoBehaviour {
    private float movementMin = 0.1f;
    void Update()
    {
        //if(Input.acceleration.x > movementMin && Input.acceleration.x > movementMin)
        //{
            transform.Rotate((Input.acceleration.x * 0.5f), (Input.acceleration.y * 0.5f), 0);
        //}
        //else if (Input.acceleration.x > movementMin)
        //{
        //    transform.Rotate(Input.acceleration.x, 0, 0);
        //}else if (Input.acceleration.y > movementMin)
        //{
        //    transform.Rotate(0, Input.acceleration.y, 0);
        //}
    }
}
