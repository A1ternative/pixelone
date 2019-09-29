using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int damage = 10;
    public string collisionTag; // tag игровых объектов с которыми мы (объект на котором размещен скрипт) будем взаимодействовать
    public Animator animator;
    public Health targetHealth;

    private void OnCollisionEnter2D(Collision2D col) // col - получаем ссылку на колайдер игрока, если скрипт висит на противнике и наоборот для игрока
    {
        if (col.gameObject.CompareTag(collisionTag))
        {
            targetHealth = col.gameObject.GetComponent<Health>();
          //  if (animator == null)
                targetHealth.TakeHit(damage);

            if (animator != null)
                animator.SetTrigger("isCharacterTouched");
        }
    }

    private void ApplyDamage()
    {
        if (targetHealth != null)
        targetHealth.TakeHit(damage);

    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(collisionTag))
        {
            Debug.Log("Пересечение коллайдеров завершено");
            if (animator != null)
                animator.SetTrigger("proceedWalking");
        }
    }

}
