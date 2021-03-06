﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inept : MonoBehaviour
{
    private Transform playerTransform;

    public Transform spawnpoint;

    public float gdspeed;

    public GameObject obj;

    private bool shooting;

    private bool canMove = true;

    public float gdcooldown;

    private SpriteRenderer spriteRenderer;

    private Animator animator;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) > 0.5f && canMove)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(playerTransform.position.x, transform.position.y), gdspeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0f) : (move.x < 0f));

        if (flipSprite)
        {

            //17 lo giriamo (opposto)
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        OnTarget();
    }

    private void OnTarget()
    {
        if (shooting == false)
        {
            if (transform.position.x > playerTransform.position.x - 0.1f && transform.position.x < playerTransform.position.x + 0.1f)
            {
                Charging();

                animator.SetTrigger("Shooting");
            }   
        }
    }

    public void Charging()
    {
        canMove = false;
        shooting = true;
    }

    public void Shoot()
    {
        Instantiate(obj, spawnpoint.transform.position, spawnpoint.transform.rotation);
        StartCoroutine("Waiting");
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(gdcooldown);

        canMove = true;
        shooting = false;
    }
}
