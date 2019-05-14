using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBash : MonoBehaviour
{
    public bool hitting;

    public GameObject toDestroy;

    public float bashForce;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && hitting)
        {
            //GetComponent<Rigidbody2D>().AddForce(Vector2.up * bashForce,ForceMode2D.Impulse);

            Destroy(toDestroy);
        }
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
}
