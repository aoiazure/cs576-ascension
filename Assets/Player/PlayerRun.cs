using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
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

        // Sprinting
        if (Input.GetButton("Run") && z > 0 && is_grounded)
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
