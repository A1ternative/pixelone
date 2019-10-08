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

    public void SetImpulse(Vector2 direction, float force)
    {
        rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
