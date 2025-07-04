using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.UIElements;
using System.Text;
using System.Security.Cryptography;
using System;

public class Shop : StaticInstance<Shop>
{

    [SerializeField] Transform abilityInventory;

    [SerializeField] Transform abilityShop;

    private Color moneyOGColor;

    [SerializeField] TextMeshProUGUI money;

    public AbilityItem abilityItemPrefab;

    public Action OnShopClose;

    public Action<ScriptableAlbilities> OnBuy;

    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        money.SetText(GameManager.Instance.currentMoney.ToString());
        GameManager.Instance.MoneyChangeObserver += () => { money.SetText(GameManager.Instance.currentMoney.ToString()); };
        gameObject.SetActive(false);
        moneyOGColor = money.color;
    }

    public void Init(List<ScriptableAlbilities> shopList, Action<ScriptableAlbilities> onBuyCallBack)
    {
        
        gameObject.SetActive(true);
        InitInventory();
        InitShop(shopList);
        OnBuy = onBuyCallBack;
    }

    public void OnEnable()
    {
        if(PlayerUnit.instance != null) 
            PlayerUnit.instance.mouseBlock = true;
        if (animator)
        {
            animator.CrossFade("ShopInit", 0f);
        }
    }

    public void StartDeInit()
    {
        PlayerUnit.instance.mouseBlock = false;
        if (animator)
        {
            animator.CrossFade("ShopDeInit", 0f);
        }
    }

    public void DeInit()
    {
        TooltipManager.Instance.Hide();
        OnShopClose?.Invoke();
        OnShopClose = null;
        OnBuy = null;
        gameObject.SetActive(false);
    }

    public void InitInventory()
    {
        foreach (Transform a in abilityInventory)
        {
            Destroy(a.gameObject);
        }
        if (PlayerUnit.instance != null)
        {
            var aList = PlayerUnit.instance.ShowAbilities();
            foreach (var a in aList)
            {
                if (a.onEvent != Event.IncreaseStat)
                {
                    var ab = Instantiate(abilityItemPrefab);
                    ab.Init(a, true, true);
                    ab.OnSell += SellAbility;
                    ab.transform.SetParent(abilityInventory);
                }
                
            }
        }
    }

    public void InitShop(List<ScriptableAlbilities> list)
    {
        foreach (Transform a in abilityShop)
        {
            Destroy(a.gameObject);
        }
        foreach (var a in list)
        {
            var ab = Instantiate(abilityItemPrefab);
            ab.Init(a, true, false);
            ab.OnBuy += BuyAbility;
            ab.transform.SetParent(abilityShop);
        }
    }

    public void BuyAbility(ScriptableAlbilities a)
    {
        PlayerUnit.instance.AddAbility(a);
        InitInventory();
        OnBuy?.Invoke(a);
    }

    public void SellAbility(ScriptableAlbilities a)
    {
        PlayerUnit.instance.DeleteAbility(a);
        InitInventory();
    }

    public void NotEnoughMoney() {
        StartCoroutine(NotEnoughMoneyAnim());
    }

    private IEnumerator NotEnoughMoneyAnim()
    {
        int count = 0;
        while (count < 60)
        {
            if (count % 3 == 0)
            {
                money.color = moneyOGColor;
            } else
            {
                money.color = Color.red;
            }
                count++;
            yield return new WaitForSeconds(1 / 60);
        }

        money.color = moneyOGColor;
    }
}
