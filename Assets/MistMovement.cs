using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistMovement : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed, 0, 0 * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        speed = -speed;
    }
}
