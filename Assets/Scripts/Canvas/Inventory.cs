using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : StaticInstance<Inventory>
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI mpText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI spdText;
    [SerializeField] private TextMeshProUGUI defText;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI scoreText;
 
    [SerializeField] private GameObject abilitiesContainer;
    [SerializeField] private AbilityIcon abilityDisplayPrefabs;

    private GameObject player;


    void DisplayStats(Stats stats) {
        hpText.SetText(stats.hp.ToString());
        mpText.SetText(stats.mp.ToString());
        atkText.SetText(stats.atk.ToString());
        spdText.SetText(stats.spd.ToString());
        defText.SetText(stats.def.ToString());

        moneyText.SetText(GameManager.Instance.currentMoney.ToString());
        scoreText.SetText(GameManager.Instance.currentStatistics.score.ToString());
    }

    void DisplayAbilities(List<ScriptableAlbilities> abilities) {
        foreach(ScriptableAlbilities ability in abilities) {
            if(ability.onEvent != Event.IncreaseStat)
                CreateAnAbilityDisplay(ability);
        }
    }

    void CreateAnAbilityDisplay(ScriptableAlbilities ability) {
        var ab = Instantiate(abilityDisplayPrefabs);
        ab.Init(ability);
        ab.transform.SetParent(abilitiesContainer.transform);
    }

    private void Start() {
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        player = GameObject.FindGameObjectWithTag("Player");

        if(player != null) {
            PlayerUnit.instance.mouseBlock = true;
            UnitBase playerBase = player.GetComponent<UnitBase>();
            if(playerBase != null) {
                DisplayStats(playerBase.ShowStats());
                DisplayAbilities(playerBase.ShowAbilities());
            }
        }
    }

    private void OnDisable() {
        if (PlayerUnit.instance != null) 
            PlayerUnit.instance.mouseBlock = false;
        ResetAbilityDisplay();
    }


    private void ResetAbilityDisplay() {
        foreach(Transform child in abilitiesContainer.transform) {
            Destroy(child.gameObject);
        }
    }

    public void GoBackButton() {
        GameManager.Instance.Menu();
    }

}
