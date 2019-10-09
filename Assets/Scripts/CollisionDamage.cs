using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;
   // [SerializeField] private string collisionTag; // tag игровых объектов с которыми мы (объект на котором размещен скрипт) будем взаимодействовать
    [SerializeField] private Animator animator;
    private Health health; // можно целиком сделать приватным, нет необходимости его как то изменять в инспекторе (пока ситуация такого не требует)
    private float direction;
    public float Direction
    {
        get { return direction; }
    }

    private void OnCollisionStay2D(Collision2D col) // col - получаем ссылку на колайдер игрока, если скрипт висит на противнике и наоборот для игрока
    {
        health = col.gameObject.GetComponent<Health>();
        if (health != null)
        {
            direction = (col.transform.position - transform.position).x;
            animator.SetFloat("Direction", Mathf.Abs(direction));
        }
    }

    private void SetDamage()
    {
        if (health != null)
            health.TakeHit(damage);
        health = null; //уничтожаем ссылку на объект, что бы не применять к нему эффект повторно
        direction = 0;
        animator.SetFloat("Direction", 0f);
    }

   


}
