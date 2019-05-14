using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //public int intensity;

    //public enum shieldZone {Up, Middle, Down }

    //public shieldZone myShieldZone;

    public float velocity;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TriggerProjectile"))
        {
            if (Input.GetButtonDown("ShieldAction"))
            {

            }
        }
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * 10, ForceMode2D.Impulse);
        // TO DO inserire controllo per evitare piattaforma
        /*if (collision.GetContact(0).point.y - gameObject.GetComponent<BoxCollider2D>().bounds.center.y > gameObject.GetComponent<BoxCollider2D>().size.y * 0.7)
        {
            myShieldZone = shieldZone.Up;
        }
        else if (collision.GetContact(0).point.y - gameObject.GetComponent<BoxCollider2D>().bounds.center.y < gameObject.GetComponent<BoxCollider2D>().size.y * 0.3)
        {
            myShieldZone = shieldZone.Down;
        }
        else
            myShieldZone = shieldZone.Middle;

        switch (myShieldZone)
        {
            case shieldZone.Up:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1,1) * 10, ForceMode2D.Impulse);
                break;

            case shieldZone.Down:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, -1) * 10, ForceMode2D.Impulse);
                break;

            case shieldZone.Middle:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * 10, ForceMode2D.Impulse);
                break;

            default:
                break;
        }*/
    }
}
