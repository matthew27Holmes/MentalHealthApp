using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{

    public GameObject[] Clouds;
    public Vector3 range;
    public float maxCloudSpeed, minCloudSpeed;


    private float timeBtwSpawn;
    public float startTimeBtwSpwan;

    public void SpawnCloud()
    {

        if (timeBtwSpawn <= 0)
        {

            int ranCloudIndex = Random.Range(0, Clouds.Length);

            Vector3 randomRange = new Vector3(Random.Range(-range.x, range.x),
                                              Random.Range(-range.y, range.y),
                                              Random.Range(-range.z, range.z));

            GameObject CloudPatten = Instantiate(Clouds[ranCloudIndex], transform.position + randomRange, Quaternion.identity);
            CloudPatten.transform.parent = transform;
            CloudPatten.transform.localScale = new Vector3(1, 1, 1);

            timeBtwSpawn = startTimeBtwSpwan;        
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        } 
    }
}
