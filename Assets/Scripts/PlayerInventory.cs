using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int coinsCount;
    public int CoinsCount
    {
        get { return coinsCount; }
        set
        {
            if (value > 0)
                coinsCount = value;
        }
    }
    [SerializeField] private Text coinsText;
    private List<Item> items;
    public List<Item> Items
    {
        get { return items; }
    }
    public BuffReciever buffReciever;


    private void Start()
    {
        GameManager.Instance.inventory = this;
        coinsText.text = CoinsCount.ToString();
        items = new List<Item>();
    }

    #region Singleton    
    public static PlayerInventory Instance { get; set;  }
    #endregion
    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (GameManager.Instance.coinContainer.ContainsKey(col.gameObject))
        {
            CoinsCount++;
            coinsText.text = CoinsCount.ToString();
            var coin = GameManager.Instance.coinContainer[col.gameObject];
            coin.StartDestroy();
        }

        if (GameManager.Instance.itemsContainer.ContainsKey(col.gameObject))  //   нужно для обратботки столкновений с обьектами, которые нас интересуют
        {
            var itemComponent = GameManager.Instance.itemsContainer[col.gameObject];
            items.Add(itemComponent.Item);
            itemComponent.Destroy(col.gameObject);
        }
    }    
}
