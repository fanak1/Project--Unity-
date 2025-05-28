using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour, ITooltip
{
    public Image icon;

    public TextMeshProUGUI nameText;

    public ScriptablePlayerUnit character;

    public void Start()
    {
        if (character != null)
        {
            icon.sprite = character.Icon;
            nameText.SetText(character.characterCode.ToString());
        }
    }

    public string ToolTipText()
    {
        Stats stats = character.base_stats;
        List<ScriptableAlbilities> abilities = character.abilities;
        string ability = "Ability: \n";
        abilities.ForEach(a => { ability += $"- {a.name}: {a.description}. \n"; });
        string description =
            "Stats: \n" +
            $"HP: {stats.hp}\n" +
            $"MP: {stats.mp}\n" +
            $"ATK: {stats.atk}\n" +
            $"SPD: {stats.spd}\n" +
            $"DEF: {stats.def}\n\n" +
            ability;

        return description;
    }

    public void StartGame()
    {
        if(character != null) GameManager.Instance.StartGame(this.character);
    }

}
