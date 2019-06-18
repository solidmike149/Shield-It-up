using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inept : MonoBehaviour
{
    public Transform playerTransform;

    public Transform spawnpoint;

    public float speed;

    public float fireRate;

    public GameObject obj;

    public bool shooting;

    public bool moving = true;

    public float cooldown;

    public float delay;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
            if (transform.position.x > playerTransform.position.x - 0.05f && transform.position.x < playerTransform.position.x + 0.05f)
            {
                StartCoroutine("SpawnProjectile");
                shooting = true;
            }   
        }
    }

    IEnumerator SpawnProjectile()
    {
        yield return new WaitForSeconds(fireRate);
        Instantiate(obj, spawnpoint.transform.position, spawnpoint.transform.rotation);
        shooting = false;
        moving = false;

        yield return new WaitForSeconds(cooldown);
        moving = true;
    }
}
