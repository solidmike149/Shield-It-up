using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayEnemyShoot : MonoBehaviour {

    public Transform startPoint;

    public GameObject obj;
    
    //Raycast variables
    public RaycastHit2D hit;

    public int distance;

    public LayerMask mask = -1;

	private void BulletInstantiate()
    {
        Instantiate(obj, startPoint.position, startPoint.rotation);
    }


	// Update is called once per frame
	void Update () {
        if (Physics2D.Raycast(startPoint.position, Vector2.right, distance, mask.value))
        {
            hit = Physics2D.Raycast(startPoint.position, Vector2.right, distance, mask.value);

            InvokeRepeating("BulletInstantiate", 0, 2);
        }
    }
}
