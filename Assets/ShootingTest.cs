using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTest : MonoBehaviour
{
    public GameObject bullet;

    public Transform startpoint;

    public RaycastHit2D hit;

    public LayerMask mask;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Instantiate(bullet,startpoint.transform.position,startpoint.transform.rotation);
        }

        if(Physics2D.Raycast(transform.position, Vector2.left, mask.value))
            hit = Physics2D.Raycast(transform.position, Vector2.left);

        if (hit.collider)
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.DrawRay(hit.point, hit.normal*100, Color.green);
        }   
    }
}