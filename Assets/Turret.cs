using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject obj;

    public Transform spawnpoint;

    private void Shoot()
    {
        Instantiate(obj, spawnpoint.position, spawnpoint.rotation);
    }
}
