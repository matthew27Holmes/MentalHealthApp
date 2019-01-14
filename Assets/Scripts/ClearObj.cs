using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObj : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<playerControler>().die();
        }
        Destroy(other.gameObject);
    }
}
