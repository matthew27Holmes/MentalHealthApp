using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerControler : MonoBehaviour {

    private Vector2 targetPos;
    private int health = 3;

    public float YMod;
    public float speed;
    public float MaxH, MinH;


    // Update is called once per frame
    void Update () {

        //transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    public void TakeDamge(int damge)
    {
        health -= damge;
        Debug.Log(health);
        if(health <= 0)
        {
            die();
        }
    }

    public void moveUp()
    {
        if (transform.position.y < MaxH)
        {
            targetPos = new Vector2(transform.position.x, transform.position.y + YMod);
            transform.position = targetPos;
        }
    }

    public void moveDown()
    {
        if (transform.position.y > MinH)
        {
            targetPos = new Vector2(transform.position.x, transform.position.y - YMod);
            transform.position = targetPos;
        }
    }

    public void die()
    {
        Debug.Log("blah i died");
        //Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
