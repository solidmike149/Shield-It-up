using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{
    public bool used;

    private Color pile;

    private void Start()
    {
        pile = GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (used)
        {
            StartCoroutine("ScrapVanishing");
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

        GetComponent<MeshRenderer>().material.color = pile;
    }
}
