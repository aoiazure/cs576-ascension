using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    private AudioSource audioSource;
    private bool is_running;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        // A/D
        float x = Input.GetAxis("Horizontal");
        // W/S
        float z = Input.GetAxis("Vertical");

        // Sprinting
        if (Input.GetButton("Run") && z > 0)
        {
            is_running = true;
        }
        else
        {
            is_running = false;
        }

        if (is_running && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (!is_running)
        {
            audioSource.Stop();
        }

    }
}
