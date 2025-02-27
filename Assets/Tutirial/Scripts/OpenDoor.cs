using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            SceneManager.LoadScene("TutorialSilhum");
        }
    }



}
