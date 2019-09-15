using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2.5f;
    public Rigidbody2D rigidBody;
    public float force;
    public float minimalHeigth;
    public bool isCheatMode;
    public GroundDetection groundDetection;

           

        // Update is called once per frame
        void Update()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        if (Input.GetKeyDown(KeyCode.Space) && groundDetection.isGrounded)
        {
            rigidBody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }

        //if (transform.position.y < minimalHeigth && isCheatMode)
        //{
        //    rigidBody.position = new Vector2(0, 0);
        //    transform.position = new Vector3(-30, 36, 0);
        //}
        //if (transform.position.y < minimalHeigth && !isCheatMode)
        //    Destroy(gameObject);
    }
}
