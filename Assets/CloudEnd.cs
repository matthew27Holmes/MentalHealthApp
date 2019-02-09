using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEnd : MonoBehaviour {
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("cloud"))
        {
            Destroy(other.gameObject);
        }
    }
}
