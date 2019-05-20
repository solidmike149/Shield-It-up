using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDeflection : MonoBehaviour
{
    public ShieldMovement shieldMovScript;

    public float deflectionSpeed;

    //Variabili per lo shield bash
    public bool hitting;
    public GameObject toDestroy;
    public float bashForce;

    IEnumerator CanBash()
    {
        hitting = true;

        yield return new WaitForSeconds(0.5f);

        hitting = false;
    }

    private void ShieldBash()
    {
        if (Input.GetKeyDown(KeyCode.E) && hitting)
        {
            switch (shieldMovScript.shieldDirection)
            {
                case ShieldMovement.Directions.N:
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * bashForce, ForceMode2D.Impulse);
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.NE:
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, -1) * bashForce, ForceMode2D.Impulse);
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.E:
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * bashForce, ForceMode2D.Impulse);
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.SE:
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * bashForce, ForceMode2D.Impulse);
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.S:
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * bashForce, ForceMode2D.Impulse);
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.SW:
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * bashForce, ForceMode2D.Impulse);
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.W:
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * bashForce, ForceMode2D.Impulse);
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.NW:
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(1, -1) * bashForce, ForceMode2D.Impulse);
                    Destroy(toDestroy);
                    break;

                default:
                    break;
            }
        }
    }

    private void Update()
    {
        ShieldBash();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TriggerProjectile"))
        {
            toDestroy = collision.gameObject;

            StartCoroutine("CanBash");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (shieldMovScript.shieldDirection)
        {
            case ShieldMovement.Directions.N:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * deflectionSpeed, ForceMode2D.Impulse);
                break;

            case ShieldMovement.Directions.NE:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * deflectionSpeed, ForceMode2D.Impulse);
                break;

            case ShieldMovement.Directions.E:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * deflectionSpeed, ForceMode2D.Impulse);
                break;

            case ShieldMovement.Directions.SE:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, -1) * deflectionSpeed, ForceMode2D.Impulse);
                break;

            case ShieldMovement.Directions.S:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * deflectionSpeed, ForceMode2D.Impulse);
                break;

            case ShieldMovement.Directions.SW:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, -1) * deflectionSpeed, ForceMode2D.Impulse);
                break;

            case ShieldMovement.Directions.W:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * deflectionSpeed, ForceMode2D.Impulse);
                break;

            case ShieldMovement.Directions.NW:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * deflectionSpeed, ForceMode2D.Impulse);
                break;

            default:
                break;
        }
    }
}
