using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

    public GameObject[] Clouds;
	
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnCloud()
    {
        int ranCloudIndex = Random.Range(0, Clouds.Length);

        GameObject Cloud = Instantiate(Clouds[ranCloudIndex], transform.position, Quaternion.identity);
        Cloud.transform.parent = transform;
    }

    void DestroyCloud()
    {

    }
}
