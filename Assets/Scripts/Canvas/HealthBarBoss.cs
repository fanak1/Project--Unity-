using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBoss : StaticInstance<HealthBarBoss>
{
    public HealthBar healthBar;

    public TextMeshProUGUI bossName;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void Init(int health, string bossName)
    {
        this.gameObject.SetActive(true);
        SetMaxHealth(health);
        this.bossName.text = bossName;
    }

    public void SetMaxHealth(int health)
    {
        healthBar.SetMaxValueWithoutResize(health);
    }

    public void SetValue(float health)
    {
        healthBar.SetValue(health);
    }

    public void Decrease(float amount)
    {
        healthBar.Decrease(amount);
    }
}
