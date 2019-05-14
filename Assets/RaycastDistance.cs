 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDistance: MonoBehaviour {

    /*
    public Transform startPoint;
    public int speed = 5;
    private Rigidbody2D rb2d;

    public float targetDistance;

    //Raycast variables
    public RaycastHit2D hit;
    public int distance;
    public LayerMask mask = -1;


	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.X))
        {
            if (Physics2D.Raycast(startPoint.position, Vector2.right, distance, mask.value))
            {
                Debug.Log("Yes");
                hit = Physics2D.Raycast(startPoint.position, Vector2.right, distance, mask.value);
                Debug.Log(hit.distance);
            }
            else
                Debug.Log("No");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            hit = Physics2D.Raycast(startPoint.position, Vector2.right, distance, mask.value);
            if(hit.collider != null)
            {
                targetDistance = hit.distance;
                Debug.Log(targetDistance);
            }
        }
	}*/
}
