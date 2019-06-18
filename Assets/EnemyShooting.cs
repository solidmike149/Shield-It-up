using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject obj;

    public Transform spawnpoint;

    public SpriteRenderer sRenderer;

    private Animator animator;

    public float deactivateTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        sRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "TriggerProjectile")
        {
            StartCoroutine("TurretShutdown");
        }
    }

    private void Shoot()
    {
        Instantiate(obj, spawnpoint.position, spawnpoint.rotation);
    }

    IEnumerator TurretShutdown()
    {
        animator.SetBool("ShutDown", true);

        sRenderer.color = Color.grey;

        yield return new WaitForSeconds(deactivateTime);

        sRenderer.color = Color.white;

        animator.SetBool("ShutDown", false);
    }
}
