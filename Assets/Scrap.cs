using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{
    public bool used;

    private Color pile;

    private PlayerPlatformer playerScript;

    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformer>();
    }

    private void Start()
    {
        pile = GetComponent<SpriteRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (used)
        {
            StartCoroutine("ScrapVanishing");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript.canHpUp = true;

            playerScript.scrapScript = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript.canHpUp = false;

            playerScript = null;
        }
    }

    private bool CheckAlpha()
    {
        if (pile.a > 0)
            return true;
        else
        {
            Destroy(gameObject);
            return false;   
        }
    }

    IEnumerator ScrapVanishing()
    {
        yield return new WaitForSeconds(1);

        yield return new WaitUntil(CheckAlpha);

        yield return new WaitForSeconds(0.1f);

        pile.a -= 0.05f;

        GetComponent<SpriteRenderer>().material.color = pile;
    }
}
