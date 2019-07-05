using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFall : MonoBehaviour {

    private PlayerPlatformer playerscript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerscript = collision.GetComponent<PlayerPlatformer>();
        playerscript.DestroyChildren();
        playerscript.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        playerscript.GetComponent<Rigidbody2D>().simulated = false;
        playerscript.gravityModifier = 0;
        playerscript.transform.Translate(new Vector3(0, 0.2f, 0));
        playerscript.animator.SetTrigger("Fall");
    }
}
