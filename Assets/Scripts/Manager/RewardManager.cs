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

    private List<ScriptableAlbilities> show;


    //Event ------------------------------------------------------------------------------------------

    public event Action<ScriptableAlbilities> OnRewardFinish;

    //-------------------------------------------------------------------------------------------------

    private void Start() {
        objectsText = new TextMeshProUGUI[objects.Length];
        for (int i = 0; i < objects.Length; i++) {
            objectsText[i] = objects[i].GetComponent<TextMeshProUGUI>();
        }
    }

    public void Choose(int index) { //Choose in abilities in UI
        OnRewardFinish?.Invoke(show[index]);
        ui.SetActive(false);
    }

    public void InitReward() { //Init reward
        show = GenerateAlbilities(abilities, objects.Length);
        ui.SetActive(true);
        for (int i = 0; i < objects.Length; i++) {
            objectsText[i].SetText(show[i].description);
        }
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
