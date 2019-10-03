using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]private int health;
    public int HealthP     //пришлось добавить P в конце, потому что нельзя называть имя свойства именем класса. ВОзникает конфликт
    {
        get { return health; }
        set
        {
            if (health > 0 && health < 1000)
                health = value;
        }
    }
        
    public void TakeHit(int damage)
    {
        health -= damage; // health = health - damage;   ,
        Debug.Log("Стало хп от урона : " + health);
                
        if (health <= 0)
            Destroy(gameObject); // destroy object where script installed
        
    }

    public void SetHealth(int bonusHealth)
    {
        health += bonusHealth; // this.health - health of Health class (line 7)
        Debug.Log("Стало хп от аптечки : " + health);

        if (health > 100)
            health = 100;
        

    }
}
