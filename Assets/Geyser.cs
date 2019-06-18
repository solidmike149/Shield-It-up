using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    public float rebound;

    public Rigidbody2D playerRb2d;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Shield"))
        {
            Debug.Log(collision.gameObject.tag);
            playerRb2d.velocity = Vector2.zero;

            playerRb2d.bodyType = RigidbodyType2D.Dynamic;
            playerRb2d.AddForce(new Vector2(0, 1) * rebound, ForceMode2D.Impulse);
            playerRb2d.bodyType = RigidbodyType2D.Kinematic;
        }
        else
            Debug.Log(collision.gameObject.tag);
    }
}
