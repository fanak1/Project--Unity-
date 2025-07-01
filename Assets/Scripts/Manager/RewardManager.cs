using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Linq;

public class RewardManager : Singleton<RewardManager>
{
    [SerializeField] private List<ScriptableAlbilities> abilities;

    [SerializeField] private Reward rewardUI;

    private List<ScriptableAlbilities> show;

    public GamePlayStatesManager gameStatesManager;

    public GameObject prefab;

    public Action callBackChooseReward;


    private void Start() {
        Init();
    }

    public void Init()
    {
        rewardUI = Reward.Instance;
    }

    public void Choose(ScriptableAlbilities ability) { //Choose in abilities in UI
        gameStatesManager.GainReward(ability);
        rewardUI.SetFatherActive(false);
    }

    public void CreateRewardObject(Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }

    public void InitReward(Action callBackChooseReward) { //Init reward
        GenerateAbilitiesList(ResourceSystem.Instance.GetClaimableAbilitiesForPlayer());
        show = GenerateAlbilities(abilities, 3);
        rewardUI.SetFatherActive(true);
        this.callBackChooseReward = callBackChooseReward;
        rewardUI.DisplayReward(show, (ScriptableAlbilities ability) => {
            PlayerUnit.instance.mouseBlock = false;
            Choose(ability);
            callBackChooseReward();
        });
    }

    public void ReInitReward()
    {
        InitReward(callBackChooseReward);
    }

    private List<ScriptableAlbilities> GenerateAlbilities(List<ScriptableAlbilities> myList, int amount) { //Generate random amounts of albilities from a List<ability> 

        List<ScriptableAlbilities> copy = new List<ScriptableAlbilities>(myList);
        List<ScriptableAlbilities> copyNormal = copy.Where(x => x.rarity == Rarity.Normal).ToList();
        List<ScriptableAlbilities> copyRare = copy.Where(x => x.rarity == Rarity.Rare).ToList();
        List<ScriptableAlbilities> copyEpic = copy.Where(x => x.rarity == Rarity.Epic).ToList();
        List<ScriptableAlbilities> copyLegendary = copy.Where(x => x.rarity == Rarity.Legendary).ToList();
        List<ScriptableAlbilities> list = new List<ScriptableAlbilities>();

        int i = 0;
        List<Rarity> posibleRarity = new List<Rarity> { Rarity.Normal, Rarity.Rare, Rarity.Legendary, Rarity.Epic };
        while(i<amount) {

            Rarity rarity = ResourceSystem.Instance.GenerateRandomRarity(posibleRarity);

            if (rarity == Rarity.Normal && copyNormal.Count > 0)
            {
                list.Add(GetRandomAbility(copyNormal));
                i++;
            }
            else if (rarity == Rarity.Rare && copyRare.Count > 0)
            {
                list.Add(GetRandomAbility(copyRare));
                i++;
            }
            else if (rarity == Rarity.Epic && copyEpic.Count > 0)
            {
                list.Add(GetRandomAbility(copyEpic));
                i++;
            }
            else if (rarity == Rarity.Legendary && copyLegendary.Count > 0)
            {
                list.Add(GetRandomAbility(copyLegendary));
                i++;
            } else
            {
                posibleRarity.Remove(rarity);
            }
        }

        return list;
    }

    private ScriptableAlbilities GetRandomAbility(List<ScriptableAlbilities> myList)
    {
       int random = UnityEngine.Random.Range(0, myList.Count);
       ScriptableAlbilities ability = myList[random];
       myList.RemoveAt(random);
       return ability;
    }

    //public List<ScriptableAlbilities> GenerateAlbilitiesWithRarity();

    public void GenerateAbilitiesList(List<ScriptableAlbilities> list) => abilities = list; //Generate new ability list for stages
}
