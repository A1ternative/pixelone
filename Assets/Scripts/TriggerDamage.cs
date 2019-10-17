﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] private bool isDestroyingAfterCollision;
    [SerializeField] private int damage;
    public int Damage

    {
        get { return damage; }
        set { damage = value; }
    }
    private GameObject parent;
    public GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }
    private IObjectDestroyer destroyer;

    public void Init(IObjectDestroyer destroyer)
    {
        this.destroyer = destroyer;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == parent)
            return;             // если мы соприкоснулись с парентом, то выходим из метода и ничего не делаем

       // var health = col.gameObject.GetComponent<Health>();
       // if (health != null) //- перепишем через GameManager
       if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))
        {
          var health = GameManager.Instance.healthContainer[col.gameObject];
          health.TakeHit(damage);
        }
        if (isDestroyingAfterCollision)
        {
            if (destroyer == null)
                Destroy(gameObject);
            else destroyer.Destroy(gameObject);
        }
    }
}

public interface IObjectDestroyer
{
    void Destroy(GameObject gameObject);
}
