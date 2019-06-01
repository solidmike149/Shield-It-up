using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrubmlingPlatform : MonoBehaviour
{
    public float delay;

    private Color platformMaterial;

    // Start is called before the first frame update
    void Start()
    {
        platformMaterial = GetComponent<MeshRenderer>().material.color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("Flashing");
        }
    }

    /*private bool CheckAlpha()
    {
        if (platformMaterial.a > 0.0f)
        {
            return true;
        }
        else
        {
            Destroy(gameObject);
            return false;
        }
    }*/

    IEnumerator Flashing()
    {
        yield return new WaitForSeconds(delay);

        //yield return new WaitUntil(CheckAlpha);

        yield return new WaitForSeconds(0.1f);

        platformMaterial.a -= 0.05f;

        GetComponent<MeshRenderer>().material.color = platformMaterial;
    }
}
