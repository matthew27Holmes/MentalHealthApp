using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenueContorls : MonoBehaviour {
    void Update()
    {
        transform.Rotate(Input.acceleration.x, Input.acceleration.y, 0);
    }
}
