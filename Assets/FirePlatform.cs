using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlatform : MonoBehaviour
{
    public float rebound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPlatformer playerscript = collision.gameObject.GetComponent<PlayerPlatformer>();

            if (!playerscript.isBurning)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * rebound;
                playerscript.isBurning = true;
                playerscript.animator.SetBool("IsBurning", true);
                playerscript.StartCoroutine("ResetAddforce"); 
            } 
            else
            {
                Destroy(collision.transform.GetChild(0).gameObject);
                collision.gameObject.GetComponent<Animator>().SetTrigger("FireDeath");
            }
        }
    }
}