using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public int gdbulletRotation;

    public GameObject obj;

    public Transform spawnpoint;

    private SpriteRenderer sRenderer;

    private Animator animator;

    public float gddeactivateTime;

    private GameObject child;

    private void Awake()
    {
        child = transform.GetChild(1).gameObject;

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
        Instantiate(obj, spawnpoint.position, spawnpoint.rotation = new Quaternion(0, gdbulletRotation, 0, 0));
    }

    IEnumerator TurretShutdown()
    {
        child.SetActive(true);

        animator.SetBool("ShutDown", true);

        sRenderer.color = Color.grey;

        yield return new WaitForSeconds(gddeactivateTime);

        sRenderer.color = Color.white;

        child.SetActive(false);

        animator.SetBool("ShutDown", false);
    }
}
