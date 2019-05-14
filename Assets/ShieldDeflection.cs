using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDeflection : MonoBehaviour
{
    public ShieldMovement shieldMovScript;

    public float velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (shieldMovScript.shieldDirection)
        {
            case ShieldMovement.Directions.N:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * velocity, ForceMode2D.Impulse);
                break;

            case ShieldMovement.Directions.S:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * velocity, ForceMode2D.Impulse);
                break;

            case ShieldMovement.Directions.E:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * velocity, ForceMode2D.Impulse);
                break;

            case ShieldMovement.Directions.W:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * velocity, ForceMode2D.Impulse);
                break;

            case ShieldMovement.Directions.NE:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * velocity, ForceMode2D.Impulse);
                break;

            case ShieldMovement.Directions.SE:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, -1) * velocity, ForceMode2D.Impulse);
                break;

            case ShieldMovement.Directions.SW:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, -1) * velocity, ForceMode2D.Impulse);
                break;

            case ShieldMovement.Directions.NW:
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * velocity, ForceMode2D.Impulse);
                break;

            default:
                break;
        }
    }
}
