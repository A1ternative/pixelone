using UnityEngine.UI;
using UnityEngine;

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

    private void Start()
    {
        coinsText.text = CoinsCount.ToString();
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
    }
}
