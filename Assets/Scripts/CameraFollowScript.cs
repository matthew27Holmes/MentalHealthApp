using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{

    public Transform Player;
    private Vector3 behindPlayer = new Vector3(0, 2, -4);
    public float angle;
    public float offset;

    private Vector3 velocity = new Vector3();


    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position,
            Player.transform.TransformPoint(behindPlayer) + Vector3.up,
            ref velocity, 0.1f);
       // BirdController birdController = Player.GetComponent<BirdController>();
       // transform.rotation = Quaternion.Euler(new Vector3(angle, birdController.pitch.y, 0));
        transform.LookAt(Player.position + (Vector3.up * offset));
    }


}
