using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject leftBorder;
    public GameObject rightBorder;
    public Rigidbody2D rigidBody;
    public GroundDetection groundDetection;
    public bool isRightDirection;
    /* не могу пока сообразить, какие условия можно было бы применить к Border'ам, rigidBody и groundDetection */

    //private Vector3 direction;
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

    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        set
        {
            if (speed > 0 && speed < 101) //при 100 волк начинает смешно метаться от leftBorder к rightBorder
                speed = value;
        }
    }



    void FixedUpdate()
    {
        if (isRightDirection && groundDetection.isGrounded)
        {
            rigidBody.velocity = Vector2.right * speed;
            if (transform.position.x > rightBorder.transform.position.x) // transform.position.x - относится к обьекту, к которому прикреплен скрипт
                isRightDirection = !isRightDirection;
                spriteRenderer.flipX = true;
        }
        else if (groundDetection.isGrounded)
        {
            rigidBody.velocity = Vector2.left * speed;
            if (transform.position.x < leftBorder.transform.position.x)
                isRightDirection = !isRightDirection;
                spriteRenderer.flipX = false;
        }
            
    }

    void Start()
    {
        Cat cat1 = new Cat();
        Cat cat2 = new Cat("Barsik", 3, 5, 30, 40);
        cat1.Meow();
        cat2.Meow();
    }
}
