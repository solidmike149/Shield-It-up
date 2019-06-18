using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectPlatform : MonoBehaviour
{
    public enum Platformdirection { N, S, E, W, NE, SE, SW, NW }

    public Platformdirection PlatformObsidian;

    public float deflectionSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TriggerProjectile"))
        {
            Rigidbody2D rb2d = collision.gameObject.GetComponent<Rigidbody2D>();

            switch (PlatformObsidian)
            {
                case Platformdirection.N:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(0, 1) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                case Platformdirection.S:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(0, -1) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                case Platformdirection.E:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(1, 0) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                case Platformdirection.W:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(-1, 0) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                case Platformdirection.NE:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(1, 1) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                case Platformdirection.SE:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(-1, -1) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                case Platformdirection.SW:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(-1, -1) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                case Platformdirection.NW:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(-1, 1) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                default:
                    break;
            }
        }
    }
}

