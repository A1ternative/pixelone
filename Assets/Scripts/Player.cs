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
            if (speed > 0.5)
                speed = value;
        }
    }
    [SerializeField] private Rigidbody2D rigidBody;
    public Rigidbody2D RigidBody
    {
        get { return rigidBody;  }
        set                                 
            /* одинаково работает и с set и закомменченным set. Пока хз какое лучше тут условие применить
             Возможно, что то аналогичное по дефолту применяется в инспекторе (типа дефолт = null,
             а не дефолт - ссылка, которую мы вручную перенесем в инспекторе*/
        {
            if (rigidBody != null)
               rigidBody = value;
        }
    }

    [SerializeField] private float force;
    public float Force
    {
        get { return force;  }
        /*  если не правильно указать set или не не указывать сериализацию, 
            то присваивается ноль и персонаж никуда не прыгает, 
            только отыгрывается анимация         */
        set

        {
            if (force > 1 && force < 30)
                force = value;
        }
    }

    [SerializeField] private float minimalHeigth;
    public float MinimalHeigth
    {
        get { return minimalHeigth; }
        set
        {
            if (minimalHeigth < -44.63f)
                minimalHeigth = value; 
            /* в моем случае низшая платформа находится на уровне у = -44.63.
            соответственно нет смысла иметь minimalHeigth больше этого значения */
        }
    }

    public bool isCheatMode;
    /*  наверно нет смысла проставлять модификатор private для этого поля, 
     *  пока не вижу опасности его как либо изменять в инспекторе
      оставим как есть? :) */
    public GroundDetection groundDetection;
    public GroundDetection GroundDetection
    {
        get { return groundDetection; }
        //set                                 // одинаково работает и с set и закомменченным set. Пока хз какое лучше тут условие применить
        //{
        //    if (groundDetection != null)
        //        groundDetection = value;
        //}
    }
    private Vector3 direction;
    [SerializeField] private Animator animator;
    public Animator Animator
    {
        get { return animator; }
        set                                 // одинаково работает и с set и закомменченным set. Пока хз какое лучше тут условие применить
        {
            if (animator != null)
                animator = value;
        }
    }
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


