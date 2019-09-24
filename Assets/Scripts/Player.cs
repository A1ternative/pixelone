using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2.5f;
    public Rigidbody2D rigidBody;
    public float force;
    public float minimalHeigth;
    public bool isCheatMode;
    public GroundDetection groundDetection;
    private Vector3 direction;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private bool isJumping;
             

        // Update is called once per frame
        void FixedUpdate()
    {
        animator.SetBool("isGrounded", groundDetection.isGrounded);
        if (!isJumping && !groundDetection.isGrounded)
            animator.SetTrigger("StartFall");
        isJumping = isJumping && !groundDetection.isGrounded;
        // transform.Translate(Vector2.right * Time.deltaTime * speed);
        direction = Vector3.zero; // (0;0)
        if (Input.GetKey(KeyCode.A))
            direction = Vector3.left; //(-1; 0)
        if (Input.GetKey(KeyCode.D))
            direction = Vector3.right; // (1;0)
        direction *= speed;
        direction.y = rigidBody.velocity.y;
        rigidBody.velocity = direction;

        if (Input.GetKeyDown(KeyCode.Space) && groundDetection.isGrounded)
        {
            rigidBody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            animator.SetTrigger("StartJump");
            isJumping = true;
        }

        animator.SetFloat("Speed", Mathf.Abs(direction.x));

        if (direction.x > 0)
            spriteRenderer.flipX = false;
        if (direction.x < 0)
            spriteRenderer.flipX = true;

        CheckFall();
    }

    void CheckFall()
    {
        if (transform.position.y < minimalHeigth && isCheatMode)
        {
            rigidBody.position = new Vector2(-30, -3);
            transform.position = new Vector3(-30, -4, 0);
        }
        if (transform.position.y < minimalHeigth && !isCheatMode)
            Destroy(gameObject);
    }
}