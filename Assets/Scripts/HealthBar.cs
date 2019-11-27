using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;
    [SerializeField] private float delta;
    private float currentHealth;
    private float healthValue;
    private Player player;

    private void Image()
    {
        
    }

    void Start()
    {
        //player = FindObjectOfType<Player>(); //- если без синглтона
        player = Player.Instance;
        healthValue = player.Health.CurrentHealth / 100.0f;
    }

    
    void Update()
    {
        //health.fillAmount = player.Health.CurrentHealth / 100.0f;
        currentHealth = player.Health.CurrentHealth / 100.0f;
        if (currentHealth > healthValue)
            healthValue += delta;
        if (currentHealth < healthValue)
            healthValue -= delta;
        if (Mathf.Abs(currentHealth - healthValue) < delta)
            healthValue = currentHealth;
        health.fillAmount = healthValue;
    }
}
