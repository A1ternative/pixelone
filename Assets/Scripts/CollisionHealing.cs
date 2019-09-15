using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHealing : MonoBehaviour
{
    public int bonusHealth = 30;
    //public string collisionTag; // tag игровых объектов с которыми мы (объект на котором размещен скрипт) будем взаимодействовать
    // теперь collisionTag не нужен
    private void OnCollisionEnter2D(Collision2D col)
    {
        Health health = col.gameObject.GetComponent<Health>();
        
        if (health != null)
        {
            health.SetHealth(bonusHealth);
            Destroy(gameObject);              
        }

        //if (col.gameObject.CompareTag(collisionTag))
        //{
        //    Health health = col.gameObject.GetComponent<Health>();
        //    health.SetHealth(bonusHealth);
        //    Destroy(gameObject);                        // аптечки исчезают
        //}
    }
}
