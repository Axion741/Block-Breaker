using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Paddle paddle;
    private bool hasStarted = false;
    private Vector3 paddleToBallVector;
    

	// Use this for initialization
	void Start () {
        paddle = GameObject.FindObjectOfType<Paddle>();
        paddleToBallVector = this.transform.position - paddle.transform.position;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasStarted)
        { //lock ball to paddle
            this.transform.position = paddle.transform.position + paddleToBallVector;

            if (Input.GetMouseButtonDown(0))
            {
                print("Mouse Button Pressed");
                hasStarted = true;
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 10f);
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D bounce)
    {
        Vector2 tweak = new Vector2(Random.Range(0f, 0.3f), Random.Range(0f, 0.3f));

        if (hasStarted == true)
        {
            GetComponent<AudioSource>().Play();
        }

        GetComponent<Rigidbody2D>().velocity += tweak;
    }
}
