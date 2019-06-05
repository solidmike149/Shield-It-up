using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDeflection : MonoBehaviour
{
    public ShieldMovement shieldMovScript;

    public PlayerPlatformer player;
    private Rigidbody2D playerRb2d;

    public float deflectionSpeed;

    //Variabili per lo shield bash
    public bool hitting;
    public GameObject toDestroy;
    public float bashForce;
    private void Start()
    {
        playerRb2d = player.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("ShieldBash"))
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

    IEnumerator CanBash()
    {
        hitting = true;

        yield return new WaitForSeconds(0.5f);

        hitting = false;
    }

    IEnumerator CanCompute()
    {
        shieldMovScript.canCompute = false;

        shieldMovScript.shieldDirection = ShieldMovement.Directions.E;

        yield return new WaitForSeconds(0.5f);

        shieldMovScript.canCompute = true;
    }

    private void ShieldBash()
    {
        {
            player.animator.SetTrigger("PlayerBash");
            switch (shieldMovScript.shieldDirection)
            {
                case ShieldMovement.Directions.N:
                    playerRb2d.AddForce(new Vector2(0, -1) * bashForce, ForceMode2D.Impulse);
                    player.transform.eulerAngles = new Vector3(0, 0, 90);
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.NE:
                    playerRb2d.AddForce(new Vector2(-1, -1) * bashForce, ForceMode2D.Impulse);
                    player.transform.eulerAngles = new Vector3(0, 0, 45);
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.E:
                    playerRb2d.AddForce(new Vector2(-1, 0) * bashForce, ForceMode2D.Impulse);
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.SE:
                    playerRb2d.AddForce(new Vector2(-1, 1) * bashForce, ForceMode2D.Impulse);
                    player.transform.eulerAngles = new Vector3(0, 0, -45);
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.S:
                    playerRb2d.AddForce(new Vector2(0, 1) * bashForce, ForceMode2D.Impulse);
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.SW:
                    playerRb2d.AddForce(new Vector2(1, 1) * bashForce, ForceMode2D.Impulse);
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.W:
                    playerRb2d.AddForce(new Vector2(1, 0) * bashForce, ForceMode2D.Impulse);
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.NW:
                    playerRb2d.AddForce(new Vector2(1, -1) * bashForce, ForceMode2D.Impulse);
                    player.transform.eulerAngles = new Vector3(0, 0, 45);
                    Destroy(toDestroy);
                    break;

                default:
                    break;
            }
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
        if (collision.gameObject.CompareTag("Floor"))
        {
            StartCoroutine("CanCompute");
        }
    }
}
