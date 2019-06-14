using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistMovement : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        //StartCoroutine("ChangeSpeed");
    }

    void Update()
    {
        transform.Translate(speed, 0, 0 * Time.deltaTime);
    }

    IEnumerator ChangeSpeed()
    {
        //Debug.Log("true");

        yield return new WaitForSeconds(4);

        speed = -speed;
    }
}
