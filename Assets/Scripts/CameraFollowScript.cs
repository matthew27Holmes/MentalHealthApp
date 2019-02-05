using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    public Transform lookAt;

    Vector3 DesiredPostion;

   public float offset= 1.5f;
   public float distance= 1.0f;

    private void Update()
    {
        DesiredPostion = lookAt.position + (-transform.forward * distance)+ (transform.up * offset);
        transform.position = Vector3.Lerp(transform.position,DesiredPostion,0.05f);

        transform.LookAt(lookAt.position + (Vector3.up * offset));
    }


}
