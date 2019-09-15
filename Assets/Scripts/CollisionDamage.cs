using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int damage = 10;
    public string collisionTag; // tag игровых объектов с которыми мы (объект на котором размещен скрипт) будем взаимодействовать

    private void OnCollisionEnter2D(Collision2D col) // col - получаем ссылку на колайдер игрока, если скрипт висит на противнике и наоборот для игрока
    {
        if (col.gameObject.CompareTag(collisionTag)) 
        {
            Health health = col.gameObject.GetComponent<Health>();
            health.TakeHit(damage);
        }

    }

}
