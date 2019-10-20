using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [SerializeField] private Animator animator;

      
    public void StartDestroy()
    {
        animator.SetTrigger("StartDestroy");
    }

    public void EndDestroy()
    {
        Destroy(gameObject);
    }
}
