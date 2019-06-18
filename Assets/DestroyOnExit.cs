using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnExit : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
            SceneManager.LoadScene(1);
        else
            Destroy(collision.gameObject);
    }
}
