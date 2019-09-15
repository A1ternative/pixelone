using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int coinsCount;
     
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {
            coinsCount++;
            Debug.Log("Количество монет: " + coinsCount); // монетки исчезают
            Destroy(col.gameObject);
        }
    }
}
