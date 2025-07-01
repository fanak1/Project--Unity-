using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityItem : MonoBehaviour, ITooltip
{
    [SerializeField] ScriptableAlbilities ability;

    [SerializeField] TextMeshProUGUI cost;

    private Color costOGColor;

    [SerializeField] Image icon;

    [SerializeField] Image filter;

    private bool playerHave = false;

    public bool isInteractable = true;

    public Action<ScriptableAlbilities> OnBuy;

    public Action<ScriptableAlbilities> OnSell;

    private void Start()
    {
        if (ability != null)
        {
            SetUI(ability);

        }

        if (!isInteractable)
        {
            GetComponent<Button>().interactable = isInteractable;
        }
    }

    public void BuyOrSell()
    {
        if (!playerHave && !CanBuy(GameManager.Instance.currentMoney))
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
        return money >= ability.cost;
    }

    private void FinishBuy()
    {
        OnBuy = null;
        GameManager.Instance.currentMoney -= ability.cost;
        GameManager.Instance.MoneyChangeObserver?.Invoke();
        Destroy(gameObject);
    }

    private void FinishSell()
    {
        OnSell = null;
        GameManager.Instance.currentMoney += ability.cost / 2;
        GameManager.Instance.MoneyChangeObserver?.Invoke();
    }

    public void Delete()
    {
        if (ability != null && !playerHave)
        {
            TrainingRoomManager.Instance.DeleteAbility(ability);
        }
    }

    public void Init(ScriptableAlbilities a, bool isInteractable = false, bool playerHave = false)
    {
        ability = a;
        SetUI(a, playerHave);
        this.playerHave = playerHave;
        this.isInteractable = isInteractable;
    }

    public void SetUI(ScriptableAlbilities a, bool playerHave = false)
    {
        if(a.icon != null)
        {
            icon.sprite = a.icon;
            icon.color = Color.white;
            var colorFilter = Registry.AbilityRarityColor(a.rarity);
            filter.color = new Color32(
                (byte)(colorFilter.r * 255f),
                (byte)(colorFilter.g * 255f),
                (byte)(colorFilter.b * 255f),
                60
            );
        }
            
        if(playerHave)
            cost.SetText((a.cost / 2).ToString());
        else
            cost.SetText(a.cost.ToString());
    }

    public string ToolTipText()
    {
        return ability.description;
    }
}
