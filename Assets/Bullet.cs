using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;

    /*private AxisMovement playerscript;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            playerscript = collision.gameObject.GetComponent<AxisMovement>();

            //playerscript.hp = playerscript.hp - 5;
        }
        else
            Destroy(collision.gameObject, 3);
    }*/

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
