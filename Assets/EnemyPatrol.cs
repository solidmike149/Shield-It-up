using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public float speed;

    public float distance;

    public LayerMask mask;

    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

        Physics2D.Raycast(transform.position, Vector2.left, distance, mask.value);

        //Debug.DrawRay(transform.position, Vector2.left, Color.red);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = -(speed);

        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }
}