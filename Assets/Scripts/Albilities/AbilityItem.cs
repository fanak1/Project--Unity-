using System;
using TMPro;
using UnityEngine;

public class AbilityItem : MonoBehaviour
{
    [SerializeField] ScriptableAlbilities ability;

    [SerializeField] TextMeshProUGUI cost;

    private Color costOGColor;

    [SerializeField] UnityEngine.UI.Image icon;

    private bool playerHave = false;

    public Action<ScriptableAlbilities> OnBuy;

    public Action<ScriptableAlbilities> OnSell;

    private void Start()
    {
        if (ability != null)
        {
            SetUI(ability);

        }
    }

    public void BuyOrSell()
    {
        if (!CanBuy(GameManager.Instance.money))
        {
            Shop.Instance.NotEnoughMoney();
            return;
        }

        if (ability != null)
        {
            if(!playerHave) {
                OnBuy?.Invoke(this.ability);
                FinishBuy();
            } else
            {
                OnSell?.Invoke(this.ability);
                FinishSell();
            }
        }
    }

    public bool CanBuy(int money)
    {
        return money > ability.cost;
    }

    private void FinishBuy()
    {
        OnBuy = null;
        GameManager.Instance.money -= ability.cost;
        GameManager.Instance.MoneyChangeObserver?.Invoke();
        Destroy(gameObject);
    }

    private void FinishSell()
    {
        OnSell = null;
        GameManager.Instance.money += ability.cost;
        GameManager.Instance.MoneyChangeObserver?.Invoke();
    }

    public void Delete()
    {
        if (ability != null && !playerHave)
        {
            TrainingRoomManager.Instance.DeleteAbility(ability);
        }
    }

    public void Init(ScriptableAlbilities a, bool playerHave = false)
    {
        ability = a;
        SetUI(a);
        this.playerHave = playerHave;
    }

    public void SetUI(ScriptableAlbilities a)
    {
        if(a.icon != null) 
            icon.sprite = a.icon;

        cost.SetText(a.cost.ToString());
    }
}
