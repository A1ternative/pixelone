using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 2.5f;
    public float Speed
    {
        get { return speed; }
        set
        {
            if (speed > 0.5f)
                speed = value;
        }
    }
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float force;
    [SerializeField] private float minimalHeigth;
    [SerializeField] private bool isCheatMode;
    /*  наверно нет смысла проставлять модификатор private для этого поля, 
     *  пока не вижу опасности его как либо изменять в инспекторе
      оставим как есть? :) */
    public GroundDetection groundDetection;
    private Vector3 direction;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isJumping;
    [SerializeField] private GameObject arrow;
       
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

    public void Update()
    {
        CheckShoot();
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

    void CheckShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(arrow);
        }
    }

    private void CoinsCollector()
    {
        PlayerInventory.Instance.CoinsCount++;
        Debug.Log("Количество монет: " + PlayerInventory.Instance.CoinsCount); // монетки исчезают 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {
            CoinsCollector();
            Destroy(col.gameObject);
        }
    }
}


