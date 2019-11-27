using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    #endregion

    [SerializeField] private GameObject inventoryPanel;
    public Dictionary<GameObject, Health> healthContainer;
    public Dictionary<GameObject, Coin> coinContainer;
    public Dictionary<GameObject, BuffReciever> buffRecieverContainer;
    public Dictionary<GameObject, ItemComponent> itemsContainer;
    [HideInInspector] public PlayerInventory inventory;
    public ItemBase itemDataBase;
           
    private void Awake()
    {
        Instance = this;
        healthContainer = new Dictionary<GameObject, Health>();
        coinContainer = new Dictionary<GameObject, Coin>();
        buffRecieverContainer = new Dictionary<GameObject, BuffReciever>();
        itemsContainer = new Dictionary<GameObject, ItemComponent>();
    }

    public void OnClickPause()
    {
        if (Time.timeScale > 0)
        {
            inventoryPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            inventoryPanel.gameObject.SetActive(false);
            Time.timeScale = 1;
        }       
    }
   
    /*private void Start()
    {
        var healthObjects = FindObjectsOfType<Health>(); //дорогая процедура в плане памяти
        foreach (var health in healthObjects)
        {
            healthContainer.Add(health.gameObject, health);
        }
    }
    перенесем более рациональную логику в скрипт health.cs*/

}
