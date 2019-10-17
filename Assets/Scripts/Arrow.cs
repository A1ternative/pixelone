using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IObjectDestroyer
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
    private Player player;

    public void Destroy(GameObject gameObject)
    {
        player.ReturnArrowToPool(this); // this - означает передать ссылку на этот скрипт (на Arrow скрипт)
    }

    public void SetImpulse(Vector2 direction, float force, Player player)
    {
        this.player = player;
        triggerDamage.Init(this);
        triggerDamage.Parent = player.gameObject; // инициализация свойства парент из скрипта триггерДамадж
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
