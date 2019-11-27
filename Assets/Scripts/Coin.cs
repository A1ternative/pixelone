using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Animator animator;


    private void Start()
    {
        GameManager.Instance.coinContainer.Add(gameObject, this); // нет вызовов дорогого FindObjects и это более оптимизированный подход 
    }

    public void StartDestroy()
    {
        animator.SetTrigger("StartDestroy");
    }

    public void EndDestroy()
    {
        Destroy(gameObject);
    }


    
}
