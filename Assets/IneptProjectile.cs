using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IneptProjectile : MonoBehaviour
{
    public float speed;

    private void OnEnable()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")||collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
