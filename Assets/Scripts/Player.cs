﻿using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Singleton
    public static Player Instance { get; private set; }
    #endregion

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
    [SerializeField] private float shootForce;
    [SerializeField] private float damageForce;
    [SerializeField] private bool isReadyToShoot;
    [SerializeField] private float shootCooldown;
    private float timerShootCooldown;
    private List<Arrow> arrowPool;
    [SerializeField] private int arrowCount = 3;
    [SerializeField] private Health health;
    public Health Health { get { return health; } }
    public Item item;
    public BuffReciever buffReciever;
    [SerializeField] private Camera playerCamera;
    private float forceBonus;
    private float damageBonus;
    private float armorBonus;
    private UICharacterController controller;
    [SerializeField] private Image fire;
    private float TimeLeftAfterFire;
    private bool isBlockMovement;


    // Update is called once per frame
    private void Awake()
    {
        Instance = this;
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
        buffReciever.OnBuffsChanged += ApplyBuffs; // подписываемся на делегат
        health.OnTakeHit += TakeHit;
    }

    void FixedUpdate()
    {
        Move();
        animator.SetFloat("Speed", Mathf.Abs(direction.x));   
        CheckFall();        
    }

    private void Move()
    {
        animator.SetBool("isGrounded", groundDetection.isGrounded);
        if (!isJumping && !groundDetection.isGrounded)
            animator.SetTrigger("StartFall");
        isJumping = isJumping && !groundDetection.isGrounded;
        // transform.Translate(Vector2.right * Time.deltaTime * speed);
        direction = Vector3.zero; // (0;0)
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A))
            direction = Vector3.left; //(-1; 0)
        if (Input.GetKey(KeyCode.D))
            direction = Vector3.right; // (1;0)
#endif
        if (controller.Left.IsPressed)
            direction = Vector3.left; //(-1; 0)
        if (controller.Right.IsPressed)
            direction = Vector3.right; // (1;0)
        direction *= speed;
        direction.y = rigidBody.velocity.y;
        if (!isBlockMovement)
            rigidBody.velocity = direction;

        if (direction.x > 0)
            spriteRenderer.flipX = false;
        if (direction.x < 0)
            spriteRenderer.flipX = true;
        /*if (Input.GetKeyDown(KeyCode.Space) && groundDetection.isGrounded)
       {
           rigidBody.AddForce(Vector2.up * (force + forceBonus), ForceMode2D.Impulse);
           animator.SetTrigger("StartJump");
           isJumping = true;
       } //перенесем в метод Jump;
       */
    }

    public void Jump()
    {
        if (groundDetection.isGrounded)
        {
            rigidBody.AddForce(Vector2.up * (force + forceBonus), ForceMode2D.Impulse);
            animator.SetTrigger("StartJump");
            isJumping = true;
        }
    }   

    public void InitUIController(UICharacterController uiController)
    {
        controller = uiController;
        controller.Jump.onClick.AddListener(Jump); //за прыжки отвечает кнопка Джмп, которая лежит в контроллере, и нам нужно подписаться на событие Jump() через AddListener()
                                                   // у юнити есть своя система событий ButtonEvents - события при нажатии на кнопку (метод типа делегата, но не делегат, нельзя использовать += для подписи)
                                                   // AddListener(Jump) - Jump без скобок, т.к. мы передаем лишь ссылку, а не вызываем его
        controller.Fire.onClick.AddListener(CheckShoot);                                                
    }

    // Action делигаты всегда всегда имеют тип войд 
    //аргумент специально пустой, такое правило для делегата
    // иначе получим ошибку в строке buffReciever.OnBuffsChanged += TestMethod; 
    private void ApplyBuffs() 
    {
        var forceBuff = buffReciever.Buffs.Find(t => t.type == BuffType.Force);
        var armorBuff = buffReciever.Buffs.Find(t => t.type == BuffType.Armor);
        var damageBuff = buffReciever.Buffs.Find(t => t.type == BuffType.Damage);
        forceBonus = forceBuff == null ? 0 : forceBuff.additiveBonus;
        armorBonus = armorBuff == null ? 0 : armorBuff.additiveBonus;
        health.SetHealth((int)armorBonus);
        damageBonus = damageBuff == null ? 0 : damageBuff.additiveBonus;
    }
    
    private void TakeHit(int damage, GameObject attacker)
    {
        //if (animator != null)
            animator.SetBool("GetDamage", true);
        animator.SetTrigger("TakeHit");
        isBlockMovement = true;
        rigidBody.AddForce(transform.position.x < attacker.transform.position.x ? 
            new Vector2(-damageForce, 0) : new Vector2(damageForce, 0), ForceMode2D.Impulse);
    }

    public void UnblockMovement() // метод применили на ивенте первого кадра для анимации Idle Player
    {
        isBlockMovement = false;
        animator.SetBool("GetDamage", false);
    }


    public void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.Instance.OnClickPause();        
        if (Input.GetKey(KeyCode.Space))
            Jump();
#endif
      
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
        if (isReadyToShoot && groundDetection.isGrounded)
        {
            Arrow currentArrow = GetArrowFromPool();
            currentArrow.SetImpulse(Vector2.right, 
                spriteRenderer.flipX ? -force * shootForce : force * shootForce, (int)damageBonus, this);
            isReadyToShoot = false;           
            StartCoroutine(StartShootCooldawn());
            animator.SetTrigger("StartShooting");
        }
    }

    public IEnumerator StartShootCooldawn()
    {
        timerShootCooldown = shootCooldown;
        while (timerShootCooldown > 0)
        {
            yield return null;
            timerShootCooldown -= Time.deltaTime;
            fire.fillAmount = 1.0f - timerShootCooldown / shootCooldown;
            //yield return null;
        }        
        isReadyToShoot = true;
        yield break;
    }
    //yield return null; // такая запись позволяет выполнять цикл не каждую секунду, а каждый кадр 
    //yield break;

    /*public IEnumerator StartShootCooldawn()
    {
        while (TimeLeftAfterFire < shootCooldown)
        {
            fire.fillAmount = (shootCooldown - TimeLeftAfterFire) / 100.0f / 100.0f;
            Debug.Log("Выстрел готов через = " + (shootCooldown - TimeLeftAfterFire) / 100.0f);
        }
        yield return new WaitForSeconds(shootCooldown);
        isReadyToShoot = true;
        yield break;
    }*/ 
    //вид корутины, через WaitForSeconds

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

    private void OnDestroy()
    {
        playerCamera.transform.parent = null;   //означает что обьект передается в корень сцены и больше не привязан к игроку
        playerCamera.enabled = true; // Unity при уничтожении обектов обнуляет компоненты. нужно их включить
    }
}


