using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField]
    private float delay = 3f;
    [SerializeField]
    private ParticleSystem particle;
    [SerializeField]
    private AudioSource sound;
    [SerializeField]
    private AudioClip fx;
    [SerializeField]
    private ParticleSystem boost;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            particle.Play();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        particle.Stop();
        boost.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            sound.PlayOneShot(fx);
            Invoke("Restart", delay);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
