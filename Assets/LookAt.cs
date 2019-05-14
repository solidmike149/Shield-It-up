using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    private Transform target;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {

        
        Vector2 targetPosition = (target.position - transform.position).normalized;

        float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;

        Quaternion rotation = new Quaternion();

        rotation.eulerAngles = new Vector3(0, 0, angle);

        transform.rotation = rotation;

        transform.right = target.position - transform.position;
    }
}
