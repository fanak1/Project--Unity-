using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityPackage : MonoBehaviour
{
    [SerializeField] ScriptableAlbilities ability;

    [SerializeField] TextMeshProUGUI description;

    [SerializeField] TextMeshProUGUI skillType;

    private bool playerHave = false;

    private void Start() {
        if(ability != null) {
            SetUI(ability);

        }
    }

    public void Choose() {
        if (ability != null && !playerHave) {
            TrainingRoomManager.Instance.AddAbility(ability);
        }
    }

    public void Init(ScriptableAlbilities a, bool playerHave = false) {
        ability = a;
        SetUI(a);
        this.playerHave = playerHave;
    }

    public void SetUI(ScriptableAlbilities a) {
        description.SetText(a.description);
        skillType.SetText(a.skillType);
        skillType.outlineWidth = 0.2f;
        skillType.outlineColor = a.skillTypeColor;
        Color32 underlayColor = a.skillTypeColor;
        underlayColor.a = 150;
        skillType.fontMaterial.SetColor("_UnderlayColor", underlayColor);
    }
}
