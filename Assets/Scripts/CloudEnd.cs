using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEnd : MonoBehaviour {
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Cloud"))
        {
            Destroy(other.gameObject);
        }
    }
}
