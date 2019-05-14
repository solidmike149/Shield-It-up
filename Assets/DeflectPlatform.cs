using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectPlatform : MonoBehaviour
{
    public enum Platformdirection { N, S, E, W, NE, SE, SW, NW }

    public Platformdirection PlatformObsidian;

    public float velocity;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TriggerProjectile"))
        {
            switch (PlatformObsidian)
            {
                case Platformdirection.N:
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * velocity, ForceMode2D.Impulse);
                    break;

                case Platformdirection.S:
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * velocity, ForceMode2D.Impulse);
                    break;

                case Platformdirection.E:
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0)* velocity, ForceMode2D.Impulse);
                    break;

                case Platformdirection.W:
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * velocity, ForceMode2D.Impulse);
                    break;

                case Platformdirection.NE:
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * velocity, ForceMode2D.Impulse);
                    break;

                case Platformdirection.SE:
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, -1) * velocity, ForceMode2D.Impulse);
                    break;

                case Platformdirection.SW:
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, -1) * velocity, ForceMode2D.Impulse);
                    break;

                case Platformdirection.NW:
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * velocity, ForceMode2D.Impulse);
                    break;

                default:
                    break;
            }
        }
    }
}

