using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;
    public Camera cam;

    public float speed = 8f;
    public float sprint_speed = 2f;
    public float gravity = -9.81f;
    public float jump_height = 3f;

    public Transform ground_check;
    public float ground_distance = 0.4f;
    public LayerMask ground_mask;

    Vector3 velocity;
    bool is_grounded;
    bool is_running;

    // Update is called once per frame
    void Update() {

        // check ground
        is_grounded = Physics.CheckSphere(ground_check.position, ground_distance, ground_mask);
        // jump
        if (Input.GetButtonDown("Jump") && is_grounded) {
            velocity.y = Mathf.Sqrt(jump_height * -2 * gravity);
        }
        // not jumping
        if (is_grounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        // A/D
        float x = Input.GetAxis("Horizontal");
        // W/S
        float z = Input.GetAxis("Vertical");
        Vector3 move;

        // Prevent A/D movement in air
        if (!is_grounded)
            move = transform.forward * z;
        else
            move = transform.right * x + transform.forward * z;
        move = move.normalized;

        // Sprinting
        if (Input.GetButton("Run") && z > 0) {
            is_running = true;
            controller.Move(move * speed * sprint_speed * Time.deltaTime);
        }
        // Not sprinting
        else {
            is_running = false;
            controller.Move(move * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
