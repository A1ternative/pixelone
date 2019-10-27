using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;
    private float healthValue;
    private Player player;

    private void Image()
    {
        
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    
    void Update()
    {
        health.fillAmount = player.Health.CurrentHealth / 100.0f;
    }
}
