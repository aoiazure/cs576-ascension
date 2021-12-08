using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    private AudioSource audioSource;
    private bool is_walking;
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

        // Walking
        if (!Input.GetButton("Run") && !Input.GetButton("Jump"))
        {
            if (x != 0 || z != 0)
            {
                is_walking = true;
                is_running = false;
            }
            else
            {
                is_walking = false;
            }
        }
        if (Input.GetButton("Run"))
        {
            is_walking = false;
        }

        if (is_walking && !is_running && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (!is_walking)
        {
            audioSource.Stop();
        }

    }
}
