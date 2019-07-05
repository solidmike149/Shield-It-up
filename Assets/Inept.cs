using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inept : MonoBehaviour
{
    public Transform playerTransform;

    public Transform spawnpoint;

    public float speed;

    public GameObject obj;

    public bool shooting;

    public bool moving = true;

    public float cooldown;

    private Animator animator;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) > 0.5f && moving)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(playerTransform.position.x, transform.position.y), speed * Time.deltaTime);
        }
    }

    private void Update()
    {
        OnTarget();
    }

    private void OnTarget()
    {
        if (shooting == false)
        {
            if (transform.position.x > playerTransform.position.x - 0.1f && transform.position.x < playerTransform.position.x + 0.1f)
            {
                StartCoroutine("SpawnProjectile");
            }   
        }
    }

    public void Shoot()
    {
        Instantiate(obj, spawnpoint.transform.position, spawnpoint.transform.rotation);
    }

    IEnumerator SpawnProjectile()
    {
        animator.SetBool("Shooting", true);
        shooting = true;
        moving = false;

        yield return new WaitForSeconds(cooldown);

        animator.SetBool("Shooting", false);
        moving = true;
        shooting = false;
    }
}
