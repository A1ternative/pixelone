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
    public GroundDetection groundDetection;
    private Vector3 direction;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isJumping;
    [SerializeField] private Arrow arrow;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private int shootForce;
    [SerializeField] private bool isReadyToShoot;
    [SerializeField] private float shootCooldown;
    private List<Arrow> arrowPool;
    [SerializeField] private int arrowCount = 3;
    [SerializeField] private Health health;
    public Health Health { get { return health; } }
    public Item item;

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

    public void Start()
    {
        arrowPool = new List<Arrow>();
        for (int i = 0; i < arrowCount; i++)
        {
            var arrowTemp = Instantiate(arrow, arrowSpawnPoint);
            arrowPool.Add(arrowTemp);
            arrowTemp.gameObject.SetActive(false);
        }
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
        if (Input.GetMouseButtonDown(0) && isReadyToShoot && groundDetection.isGrounded)
        {
            Arrow currentArrow = GetArrowFromPool();
            currentArrow.SetImpulse(Vector2.right, 
                spriteRenderer.flipX ? -force * shootForce : force * shootForce, this);
            isReadyToShoot = false;
            StartCoroutine(StartShootCooldawn());
            animator.SetTrigger("StartShooting");
        }
    }

    private IEnumerator StartShootCooldawn()
    {
        yield return new WaitForSeconds(shootCooldown);
        isReadyToShoot = true;
        Debug.Log(isReadyToShoot);
        yield break;
    }
    //yield return null; // такая запись позволяет выполнять цикл не каждую секунду, а каждый кадр 
    //yield break;

    private Arrow GetArrowFromPool()
    {
        if (arrowPool.Count > 0)
        {
            var arrowTemp = arrowPool[0];
            arrowPool.Remove(arrowTemp);
            arrowTemp.gameObject.SetActive(true);
            arrowTemp.transform.parent = null;
            arrowTemp.transform.position = arrowSpawnPoint.transform.position;
            return arrowTemp;
        }
        return Instantiate(arrow, arrowSpawnPoint.position, Quaternion.identity);

    }

    public void ReturnArrowToPool(Arrow arrowTemp)
    {
        if (!arrowPool.Contains(arrowTemp)) // если текущей стрелы нет в пуле, то мы её добавляем в пулл
        {
            arrowPool.Add(arrowTemp);

            arrowTemp.transform.parent = arrowSpawnPoint;
            arrowTemp.transform.position = arrowSpawnPoint.transform.position;
            arrowTemp.gameObject.SetActive(false);
        }
    }

  

}


