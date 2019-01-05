using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticelBehaviour : MonoBehaviour {

    public float speed;
    public int damage;
    private AudioProcessor AP;
    public GameObject effect;


	// Use this for initialization
	void Start () {
        AP = GameObject.FindObjectOfType<AudioProcessor>();
        
	}
	
	// Update is called once per frame
	void Update () {
       // speed = AP.tapTempo();
        // Move the object forward along its z axis 1 unit/second.
        //transform.Translate(Vector3.back * (speed * Time.deltaTime));
        transform.Translate(Vector2.left * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Instantiate(effect, transform.position, Quaternion.identity);
            //other.GetComponent<playerControler>().TakeDamge(damage);
            //Destroy(gameObject);
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Instantiate(effect, transform.position, Quaternion.identity);
            //other.GetComponent<playerControler>().TakeDamge(damage);
            //Destroy(gameObject);
            other.transform.parent = other.transform;
        }
    }
}
