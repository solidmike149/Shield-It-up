﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsCamera : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.layer = 21;
    }
}
