using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int coinsCount;
    #region Singleton    
    public static PlayerInventory Instance { get; set;  }
    #endregion
    private void Awake()
    {
        Instance = this;
    }    
}
