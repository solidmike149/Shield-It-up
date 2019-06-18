using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistMovement : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        StartCoroutine("ChangeSpeed");
    }

    void Update()
    {
        transform.Translate(speed, 0, 0 * Time.deltaTime);
    }

    IEnumerator ChangeSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            speed = -speed;
        }
    }
}
