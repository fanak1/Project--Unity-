using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class AbilityPackage : MonoBehaviour
{
    [SerializeField] ScriptableAlbilities ability;

    [SerializeField] TextMeshProUGUI description;

    [SerializeField] TextMeshProUGUI skillType;

    [SerializeField] UnityEngine.UI.Image icon;

    [SerializeField] UnityEngine.UI.Image filter;

    private bool playerHave = false;

    private bool forTraining = true;

    public Action<ScriptableAlbilities> OnRewardChoose;

    public GameObject borderUpdate;

    public GameObject iconUpdate;

    public void Choose() {
        if (ability != null && !playerHave) {
            if(forTraining) TrainingRoomManager.Instance.AddAbility(ability);
            else
            {
                OnRewardChoose?.Invoke(this.ability);
                FinishChoose();
            }
        }
    }

    private void FinishChoose()
    {
        OnRewardChoose = null;
    }

    public void Delete() {
        if (ability != null && !playerHave) {
            TrainingRoomManager.Instance.DeleteAbility(ability);
        }
    }

    public void Init(ScriptableAlbilities a, bool playerHave = false, bool forTraining = false) {
        ability = a;

        var pList = PlayerUnit.instance.ShowAbilities();

        bool update = false;

        foreach(var ab in pList)
        {
            if(ab.skillType == "None") continue;
            if (ab.skillType == a.skillType && a.rarity > ab.rarity)
            {
                update = true;
                break;
            }
        }

        SetUI(a, update);
        this.playerHave = playerHave;
        this.forTraining = forTraining;
    }

    public void SetUI(ScriptableAlbilities a, bool update = false) {
        description.SetText(a.description);
        skillType.SetText(a.skillType);
        skillType.outlineWidth = 0.2f;
        skillType.outlineColor = a.skillTypeColor;
        Color32 underlayColor = a.skillTypeColor;
        underlayColor.a = 150;
        skillType.fontMaterial.SetColor("_UnderlayColor", underlayColor);
        if(a.icon != null) 
            icon.sprite = a.icon;
        var filterColor = Registry.AbilityRarityColor(a.rarity);
        filter.color = new Color32(
            (byte)(filterColor.r * 255),
            (byte)(filterColor.g * 255),
            (byte)(filterColor.b * 255),
            60
        );
        if(update)
        {
            borderUpdate.SetActive(true);
            iconUpdate.SetActive(true);
        }
        else
        {
            borderUpdate.SetActive(false);
            iconUpdate.SetActive(false);
        }
    }
}
