using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputerPlayerPanel : MonoBehaviour
{

    [SerializeField] private Transform allAbilityPanel;

    [SerializeField] private Transform playerAbilityPanel;

    [SerializeField] private List<ScriptableAlbilities> allAbilities;

    [SerializeField] private List<ScriptableAlbilities> playerAbilities;

    [SerializeField] private TextMeshProUGUI baseStatsText;

    [SerializeField] private TextMeshProUGUI increaseStatsText;

    [SerializeField] private AbilityPackage abilityPrefab;

    [SerializeField] private AbilityPackage thunderAbilityPrefab;

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
        AllAbilityDisplay();
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
            CreateAndAddAbilityToTransform(ability, playerAbilityPanel, true);
        }
    }

    private void AllAbilityDisplay() {
        allAbilities = ResourceSystem.Instance.GetAllAbilities();

        foreach(Transform child in allAbilityPanel) {
            Destroy(child.gameObject);
        }

        foreach(var ability in allAbilities) {
            CreateAndAddAbilityToTransform(ability, allAbilityPanel);
        }
    }

    private void CreateAndAddAbilityToTransform(ScriptableAlbilities ability, Transform parent, bool playerHave = false) {
        AbilityPackage a;
        switch (ability.characterCode) {
            case CharacterCode.Thunder:
                a = Instantiate(thunderAbilityPrefab);
                a.Init(ability, playerHave);
                a.gameObject.transform.SetParent(parent);
                break;
            default:
                a = Instantiate(abilityPrefab);
                a.Init(ability, playerHave);
                a.gameObject.transform.SetParent(parent);
                break;
        }
    }

    string StringifyStats(int stat) {
        return "" + (stat >= 0 ? "+" : "") + stat;
    }
}
