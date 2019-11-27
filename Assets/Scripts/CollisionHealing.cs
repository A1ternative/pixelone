using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHealing : MonoBehaviour
{
    [SerializeField] private int bonusHealth = 30;
    public int BonusHealth
    {
        get { return bonusHealth; }
        set
        {
            if (value > 0)
                bonusHealth = value;
        }
    }
    [SerializeField] private Animator animator;
    
    //public string collisionTag; // tag игровых объектов с которыми мы (объект на котором размещен скрипт) будем взаимодействовать
    // теперь collisionTag не нужен
    private void OnCollisionEnter2D(Collision2D col)
    {
        var health = GameManager.Instance.healthContainer[col.gameObject];
        
        if (health != null)
        {
            health.SetHealth(bonusHealth);
            animator.SetTrigger("StartDestroy");             
        }

        //if (col.gameObject.CompareTag(collisionTag))
        //{
        //    Health health = col.gameObject.GetComponent<Health>();
        //    health.SetHealth(bonusHealth);
        //    Destroy(gameObject);                        // аптечки исчезают
        //}
    }
}
