using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlatform : MonoBehaviour
{
  
    public float velocity;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPlatformer playerscript = collision.gameObject.GetComponent<PlayerPlatformer>();

            if (playerscript.isBurning)
            {
                Destroy(collision.gameObject);
            } 
            else
            {
                collision.rigidbody.bodyType = RigidbodyType2D.Dynamic;
                collision.rigidbody.freezeRotation = true;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * velocity, ForceMode2D.Impulse);
                playerscript.isBurning = true;
            }
        }
    }

}


