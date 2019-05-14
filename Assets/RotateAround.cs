using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

    public float speedAngle;

    private Transform target;

    private Vector3 zAxis = new Vector3 (0, 0, 1);


	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(target.position, zAxis, speedAngle * Time.deltaTime);
	}
}
