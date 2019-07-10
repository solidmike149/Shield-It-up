using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    public float rebound;

    public Rigidbody2D playerRb2d;

    public float speed;

    private void Start()
    {
        StartCoroutine("ChangeSpeed");
    }

    void Update()
    {
        transform.Translate(0,speed, 0 * Time.deltaTime);
    }

    IEnumerator ChangeSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.8f);

            speed = -speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerRb2d = collision.GetComponent<Rigidbody2D>();
        playerRb2d.velocity = Vector2.zero;
    }
}
