using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Shield" || collision.gameObject.tag !="Obsidian")
        {
            Destroy(gameObject);
        }
    }
}
