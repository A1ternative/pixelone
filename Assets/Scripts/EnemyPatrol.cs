using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject leftBorder;
    public GameObject rightBorder;
    public Rigidbody2D rigidbody;
    public GroundDetection groundDetection;
    public bool isRightDirection;
    //private Vector3 direction;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public float speed;

    void FixedUpdate()
    {
        if (isRightDirection && groundDetection.isGrounded)
        {
            rigidbody.velocity = Vector2.right * speed;
            if (transform.position.x > rightBorder.transform.position.x) // transform.position.x - относится к обьекту, к которому прикреплен скрипт
                isRightDirection = !isRightDirection;
                spriteRenderer.flipX = true;
        }
        else if (groundDetection.isGrounded)
        {
            rigidbody.velocity = Vector2.left * speed;
            if (transform.position.x < leftBorder.transform.position.x)
                isRightDirection = !isRightDirection;
                spriteRenderer.flipX = false;
        }
            
    }

    void Start()
    {
        Cat cat1 = new Cat();
        cat1.Meow();
    }
}
