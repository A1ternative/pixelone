using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public bool isGrounded;

    //private void OnCollisionEnter2D(Collision2D col) // персонаж может переходить в анимацию падения при движении по платформам
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platfrom"))
            isGrounded = true;
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platfrom"))
            isGrounded = false;
    }
}
