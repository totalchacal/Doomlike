using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 0;
    public float gravity = 50f;
    public Transform groundCheck;
    public float groundDistance = 1f;
    public LayerMask groundMask;
    public float jumpHeight = 7f;

    private Vector3 velocity;
    bool isGrounded;
    private RaycastHit hit;
    private float timeleft = 0f;
    private bool isClimbing = false;

    // Update is called once per frame
    void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
    }

    private void Move(float x, float z) {
        if (Input.GetButton("Fire3"))
            speed = 7;
        else
            speed = 5;
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    private void Jump() {
        if (Input.GetKeyDown("space") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);

        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}