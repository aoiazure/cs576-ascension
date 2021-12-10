using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    private AudioSource audioSource;
    public Transform ground_check;
    public float ground_distance = 0.4f;
    public LayerMask ground_mask;
    private bool is_walking;
    private bool is_running;
    private bool is_grounded;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        // check ground
        is_grounded = Physics.CheckSphere(ground_check.position, ground_distance, ground_mask);
        // A/D
        float x = Input.GetAxis("Horizontal");
        // W/S
        float z = Input.GetAxis("Vertical");

        // Walking
        if (!Input.GetButton("Run") && !Input.GetButton("Jump") && is_grounded)
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
        else
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
