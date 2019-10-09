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

    public void SetImpulse(Vector2 direction, float force)
    {
        rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
        StartCoroutine(StartLife());
    }

    private IEnumerator StartLife()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
        yield break;
    }
}
