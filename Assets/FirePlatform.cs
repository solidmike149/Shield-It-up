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
                collision.rigidbody.bodyType = RigidbodyType2D.Dynamic;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * rebound, ForceMode2D.Impulse);
                playerscript.isBurning = true;
                playerscript.animator.SetBool("IsBurning", true);
                playerscript.StartCoroutine("ResetAddforce");
                collision.rigidbody.bodyType = RigidbodyType2D.Kinematic;
                
            } 
            else
            {
                Destroy(collision.transform.GetChild(0).gameObject);
                collision.gameObject.GetComponent<Animator>().SetTrigger("Dead");
            }
        }
    }
}