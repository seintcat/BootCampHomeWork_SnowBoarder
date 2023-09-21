using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    private float delay = 3f;
    [SerializeField]
    private ParticleSystem particle;
    [SerializeField]
    private AudioSource sound;
    [SerializeField]
    private AudioClip fx;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sound.PlayOneShot(fx);
            Invoke("Restart", delay);
            particle.Play();
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
