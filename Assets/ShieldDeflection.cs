using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDeflection : MonoBehaviour
{
    public ShieldMovement shieldMovScript;
    public PlayerPlatformer player;
    private Rigidbody2D playerRb2d;

    private Vector2 zero = Vector2.zero;

    public float resetVelocity;

    public float deflectionSpeed;

    public float rebound;

    //Variabili per lo shield bash
    public bool hitting;
    public GameObject toDestroy;
    public float bashForce;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformer>();

        playerRb2d = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("ShieldBash"))
            ShieldBash();
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
        player.animator.SetTrigger("PlayerBash");
        if (hitting)
        {
            switch (shieldMovScript.shieldDirection)
            {
                case ShieldMovement.Directions.N:
                    playerRb2d.velocity = Vector2.zero;
                    playerRb2d.bodyType = RigidbodyType2D.Dynamic;
                    playerRb2d.AddForce(new Vector2(0, -1) * bashForce, ForceMode2D.Impulse);
                    playerRb2d.bodyType = RigidbodyType2D.Kinematic;
                    player.StartCoroutine("ResetAddforce");
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.NE:
                    playerRb2d.velocity = Vector2.zero;
                    playerRb2d.bodyType = RigidbodyType2D.Dynamic;
                    playerRb2d.AddForce(new Vector2(-1, -1) * bashForce, ForceMode2D.Impulse);
                    playerRb2d.bodyType = RigidbodyType2D.Kinematic;
                    player.StartCoroutine("ResetAddforce");
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.E:
                    playerRb2d.velocity = Vector2.zero;
                    playerRb2d.bodyType = RigidbodyType2D.Dynamic;
                    playerRb2d.AddForce(new Vector2(-1, 0) * bashForce, ForceMode2D.Impulse);
                    playerRb2d.bodyType = RigidbodyType2D.Kinematic;
                    player.StartCoroutine("ResetAddforce");
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.SE:
                    playerRb2d.velocity = Vector2.zero;
                    playerRb2d.bodyType = RigidbodyType2D.Dynamic;
                    playerRb2d.AddForce(new Vector2(-1, 1) * bashForce, ForceMode2D.Impulse);
                    playerRb2d.bodyType = RigidbodyType2D.Kinematic;
                    player.StartCoroutine("ResetAddforce");
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.S:
                    playerRb2d.velocity = Vector2.zero;
                    playerRb2d.bodyType = RigidbodyType2D.Dynamic;
                    playerRb2d.AddForce(new Vector2(0, 1) * bashForce, ForceMode2D.Impulse);
                    playerRb2d.bodyType = RigidbodyType2D.Kinematic;
                    player.StartCoroutine("ResetAddforce");
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.SW:
                    playerRb2d.velocity = Vector2.zero;
                    playerRb2d.bodyType = RigidbodyType2D.Dynamic;
                    playerRb2d.AddForce(new Vector2(1, 1) * bashForce, ForceMode2D.Impulse);
                    playerRb2d.bodyType = RigidbodyType2D.Kinematic;
                    player.StartCoroutine("ResetAddforce");
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.W:
                    playerRb2d.velocity = Vector2.zero;
                    playerRb2d.bodyType = RigidbodyType2D.Dynamic;
                    playerRb2d.AddForce(new Vector2(1, 0) * bashForce, ForceMode2D.Impulse);
                    playerRb2d.bodyType = RigidbodyType2D.Kinematic;
                    player.StartCoroutine("ResetAddforce");
                    Destroy(toDestroy);
                    break;

                case ShieldMovement.Directions.NW:
                    playerRb2d.velocity = Vector2.zero;
                    playerRb2d.bodyType = RigidbodyType2D.Dynamic;
                    playerRb2d.AddForce(new Vector2(1, -1) * bashForce, ForceMode2D.Impulse);
                    playerRb2d.bodyType = RigidbodyType2D.Kinematic;
                    player.StartCoroutine("ResetAddforce");
                    Destroy(toDestroy);
                    break;

                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TriggerProjectile"))
        {
            toDestroy = collision.gameObject;

            StartCoroutine("CanBash");
        }
        if (collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "TriggerProjectile")
        {
            Rigidbody2D rb2d = collision.gameObject.GetComponent<Rigidbody2D>();

            switch (shieldMovScript.shieldDirection)
            {
                case ShieldMovement.Directions.N:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(0, 1) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                case ShieldMovement.Directions.NE:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(1, 1) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                case ShieldMovement.Directions.E:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(1, 0) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                case ShieldMovement.Directions.SE:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(1, -1) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                case ShieldMovement.Directions.S:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(0, -1) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                case ShieldMovement.Directions.SW:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(-1, -1) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                case ShieldMovement.Directions.W:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(-1, 0) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                case ShieldMovement.Directions.NW:
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(-1, 1) * deflectionSpeed, ForceMode2D.Impulse);
                    break;

                default:
                    break;
            }
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            StartCoroutine("CanCompute");
        }
        /*else if (collision.gameObject.CompareTag("Geyser"))
        {
            playerRb2d.velocity = Vector2.zero;

            playerRb2d.bodyType = RigidbodyType2D.Dynamic;
            playerRb2d.AddForce(new Vector2(0, 1) * rebound, ForceMode2D.Impulse);
            playerRb2d.bodyType = RigidbodyType2D.Kinematic;
        }*/
    }
}
