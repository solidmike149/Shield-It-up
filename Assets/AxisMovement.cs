using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisMovement : MonoBehaviour {

    private Rigidbody2D rb2d;

    private Vector2 velocity;

    public float speed;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        speed = 5;
    }

    // Update is called once per frame
    void FixedUpdate () {
        Vector2 move = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        velocity = move.normalized * speed;

        rb2d.MovePosition (rb2d.position + velocity * Time.fixedDeltaTime);
		
	}
}
