using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class RewardManager : PersistentSingleton<RewardManager>
{
    [SerializeField] private List<ScriptableAlbilities> abilities;

    [SerializeField] private Reward rewardUI;

    private List<ScriptableAlbilities> show;


    private void Start() {
    }

    public void Choose(int index) { //Choose in abilities in UI
        GameManager.Instance.GainReward(show[index]);
        rewardUI.gameObject.SetActive(false);
    }

    public void InitReward() { //Init reward
        show = GenerateAlbilities(abilities, 3);
        rewardUI.gameObject.SetActive(true);
        rewardUI.DisplayReward(show);
    }

    private List<ScriptableAlbilities> GenerateAlbilities(List<ScriptableAlbilities> myList, int amount) { //Generate random amounts of albilities from a List<ability> 

        List<ScriptableAlbilities> copy = new List<ScriptableAlbilities>(myList);
        List<ScriptableAlbilities> list = new List<ScriptableAlbilities>();

        for(int i=0; i<amount; i++) {
            int random = UnityEngine.Random.Range(0, copy.Count);
            list.Add(copy[random]);
            copy.RemoveAt(random);
        }

        return list;
    }

    //public List<ScriptableAlbilities> GenerateAlbilitiesWithRarity();

    public void GenerateAbilitiesList(List<ScriptableAlbilities> list) => abilities = list; //Generate new ability list for stages
}
