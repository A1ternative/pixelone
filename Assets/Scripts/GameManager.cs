using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    #endregion

    public Dictionary<GameObject, Health> healthContainer;
    public Dictionary<GameObject, Coin> coinContainer;
    public Dictionary<GameObject, BuffReciever> buffRecieverContainer;
           
    private void Awake()
    {
        Instance = this;
        healthContainer = new Dictionary<GameObject, Health>();
        coinContainer = new Dictionary<GameObject, Coin>();
        buffRecieverContainer = new Dictionary<GameObject, BuffReciever>();
    }

    public void OnClickPause()
    {
        if (Time.timeScale > 0)
            Time.timeScale = 0;
        else Time.timeScale = 1;
                
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
