using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwanPoint : MonoBehaviour {

    public GameObject obsticle;
	// Use this for initialization
	void Start () {
        Instantiate(obsticle, transform.position, Quaternion.identity);
	}
}
