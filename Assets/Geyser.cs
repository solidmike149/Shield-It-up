using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    public float rebound;

    public Rigidbody2D playerRb2d;


    IEnumerator ResetGeyserForce()
    {
        yield return new WaitForSeconds(1f);

        playerRb2d.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            playerRb2d.velocity = Vector2.zero;

            playerRb2d.bodyType = RigidbodyType2D.Dynamic;
            playerRb2d.AddForce(new Vector2(0, 1) * rebound, ForceMode2D.Impulse);
            playerRb2d.bodyType = RigidbodyType2D.Kinematic;
            StartCoroutine("ResetGeyserForce");
        }
    }
}
