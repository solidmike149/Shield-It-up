﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb2d;

    public enum Bulletdirection { N, NE, E, SE, S, SW, W, NW}

    public Bulletdirection myBullet;

    private void Shoot(Bulletdirection dir)
    {
        switch (dir)
        {
            case Bulletdirection.N:
                rb2d.AddForce(new Vector2(0, 1) * speed, ForceMode2D.Impulse);
                break;

            case Bulletdirection.NE:
                rb2d.AddForce(new Vector2(1, 1) * speed, ForceMode2D.Impulse);
                break;

            case Bulletdirection.E:
                rb2d.AddForce(new Vector2(1, 0) * speed, ForceMode2D.Impulse);
                break;

            case Bulletdirection.SE:
                rb2d.AddForce(new Vector2(1, -1) * speed, ForceMode2D.Impulse);
                break;

            case Bulletdirection.S:
                rb2d.AddForce(new Vector2(0, -1) * speed, ForceMode2D.Impulse);
                break;

            case Bulletdirection.SW:
                rb2d.AddForce(new Vector2(-1, -1) * speed, ForceMode2D.Impulse);
                break;

            case Bulletdirection.W:
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(-1, 0) * speed, ForceMode2D.Impulse);
                break;

            case Bulletdirection.NW:
                rb2d.AddForce(new Vector2(-1, 1) * speed, ForceMode2D.Impulse);
                break;
        }
    }

    private void OnEnable()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        Shoot(myBullet);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Shield" && collision.gameObject.tag !="Obsidian")
        {
            GetComponent<Animator>().SetTrigger("Explode");
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
