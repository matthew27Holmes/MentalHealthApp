using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwaning : MonoBehaviour {

    public GameObject [] obstacle;

    private float timeBtwSpawn;
    public float startTimeBetweenSpwan;
    public float decreaseTime;
    public float minTime = 0.65f;

	// Update is called once per frame
	void Update () {

	}

    public void Spawn()
    {
       // if (timeBtwSpawn <= 0)
       // {
            int rand = Random.Range(0, obstacle.Length);
            Instantiate(obstacle[rand], transform.position, transform.rotation);
          //  timeBtwSpawn = startTimeBetweenSpwan;
            if (startTimeBetweenSpwan > minTime)
            {
                startTimeBetweenSpwan -= decreaseTime;
            }
      //  }
      //  else
        //{
     //       timeBtwSpawn -= Time.deltaTime;
     //   }
    }
}
