using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] private float force;
    public float Force
    {
        get { return force; }
        set { force = value; }
    }
    [SerializeField] private float lifetime;
    [SerializeField] private TriggerDamage triggerDamage;


    public void SetImpulse(Vector2 direction, float force, GameObject parent)
    {
        triggerDamage.Parent = parent; // инициализация свойства парент из скрипта триггерДамадж
        rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
        if (force < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0); //отражение стрелы относительно оси Y 
        StartCoroutine(StartLife());
        
    }

    private IEnumerator StartLife()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
        yield break;
    }

    
}
