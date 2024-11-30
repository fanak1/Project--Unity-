using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityPackage : MonoBehaviour
{
    [SerializeField] ScriptableAlbilities ability;

    [SerializeField] TextMeshProUGUI description;

    private bool playerHave = false;

    private void Start() {
        if(ability != null) {
            description.SetText(ability.description);
        }
    }

    public void Choose() {
        if (ability != null && !playerHave) {
            TrainingRoomManager.Instance.AddAbility(ability);
        }
    }

    public void Init(ScriptableAlbilities a, bool playerHave = false) {
        ability = a;
        description.SetText(a.description);
        this.playerHave = playerHave;
    }
}
