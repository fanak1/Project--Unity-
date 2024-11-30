using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputerPlayerPanel : MonoBehaviour
{

    [SerializeField] private Transform allAbilityPanel;

    [SerializeField] private Transform playerAbilityPanel;

    [SerializeField] private List<ScriptableAlbilities> playerAbilities;

    [SerializeField] private TextMeshProUGUI baseStatsText;

    [SerializeField] private TextMeshProUGUI increaseStatsText;

    [SerializeField] private AbilityPackage abilityPrefab;

    private bool init = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!init) {
            Init();
        }
        if (TrainingRoomManager.Instance.changeStats) IncreaseStatsDisplay();
        if (TrainingRoomManager.Instance.changeAbility) ResetPlayerAbility();
    }

    void Init() {
        init = true;
        ResetPlayerAbility();
        BaseStatsDisplay();
        IncreaseStatsDisplay();
    }

    void BaseStatsDisplay() {
        Stats baseStats = TrainingRoomManager.Instance.ShowBaseStats();

        baseStatsText.SetText($"{baseStats.hp}\n{baseStats.mp}\n{baseStats.spd}\n{baseStats.atk}\n{baseStats.def}");
    }
    
    void IncreaseStatsDisplay() {
        TrainingRoomManager.Instance.changeStats = false;

        Stats increaseStats = TrainingRoomManager.Instance.ShowIncreaseStats();

        increaseStatsText.SetText($"{StringifyStats(increaseStats.hp)}" +
                                    $"\n{StringifyStats(increaseStats.mp)}" +
                                    $"\n{StringifyStats(increaseStats.spd)}" +
                                    $"\n{StringifyStats(increaseStats.atk)}" +
                                    $"\n{StringifyStats(increaseStats.def)}");
    }

    void ResetPlayerAbility() {
        TrainingRoomManager.Instance.changeAbility = false;

        foreach(Transform child in playerAbilityPanel) {
            Destroy(child.gameObject);
        }
        playerAbilities = TrainingRoomManager.Instance.GetPlayerAbility();

        foreach (var ability in playerAbilities) {
            AbilityPackage a = Instantiate(abilityPrefab);
            a.Init(ability, true);
            a.gameObject.transform.SetParent(playerAbilityPanel);
        }
    }

    string StringifyStats(int stat) {
        return "" + (stat >= 0 ? "+" : "") + stat;
    }
}
