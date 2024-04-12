using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class RewardManager : PersistentSingleton<RewardManager>
{
    [SerializeField] private GameObject[] objects;

    [SerializeField] private TextMeshProUGUI[] objectsText;

    [SerializeField] private List<ScriptableAlbilities> abilities;

    [SerializeField] private GameObject ui;


    //Event ------------------------------------------------------------------------------------------

    public event Action<ScriptableAlbilities> OnRewardFinish;

    //-------------------------------------------------------------------------------------------------

    private void Start() {
        objectsText = new TextMeshProUGUI[objects.Length];
        for (int i = 0; i < objects.Length; i++) {
            objectsText[i] = objects[i].GetComponent<TextMeshProUGUI>();
        }
    }

    public void Choose(int index) {
        OnRewardFinish?.Invoke(abilities[index]);
        ui.SetActive(false);
    }

    public void InitReward() { 
        ui.SetActive(true);
        for (int i = 0; i < objects.Length; i++) {
            objectsText[i].SetText(abilities[i].description);
        }
    }

    private List<ScriptableAlbilities> GenerateAlbilities(int amount) {
        List<ScriptableAlbilities> list = new List<ScriptableAlbilities>();
        foreach (ScriptableAlbilities a in  abilities) {
            if(a.rarity == Rarity.Normal) {
                list.Add(a);
            }
        }
        return list;
    }
}
