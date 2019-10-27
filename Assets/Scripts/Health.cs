using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Animator animator;
    public int CurrentHealth
    {
        get { return health; }
    }

    private void Start()
    {
        GameManager.Instance.healthContainer.Add(gameObject, this); // нет вызовов дорогого FindObjects и это более оптимизированный подход 
    }

    public void TakeHit(int damage)
    {
        health -= damage; // health = health - damage;   ,
        //Debug.Log("Стало хп от урона : " + health);
        if (animator != null)
            animator.SetTrigger("TakingDamage");

        if (health <= 0)
            Destroy(gameObject); // destroy object where script installed
        
    }

    public void SetHealth(int bonusHealth)
    {
        health += bonusHealth; // this.health - health of Health class (line 7)
        //Debug.Log("Стало хп от аптечки : " + health);

        if (health > 100)
            health = 100;     
    }
}
