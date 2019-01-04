using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticelBehaviour : MonoBehaviour {

    public float speed;
    public int damage;

    public GameObject effect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Move the object forward along its z axis 1 unit/second.
        //transform.Translate(Vector3.back * (speed * Time.deltaTime));
        transform.Translate(Vector2.left * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
       /* if(other.CompareTag("Player"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            other.GetComponent<playerControler>().TakeDamge(damage);
            Destroy(gameObject);
        }*/
    }
}
