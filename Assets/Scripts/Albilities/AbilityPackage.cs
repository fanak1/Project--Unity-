using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityPackage : MonoBehaviour
{
    [SerializeField] ScriptableAlbilities ability;

    [SerializeField] TextMeshProUGUI description;

    private void Start() {
        if(ability != null) {
            description.SetText(ability.description);
        }
    }

    public void Choose() {
        if(ability != null) TrainingRoomManager.Instance.AddAbility(ability);
    }

    public void Init(ScriptableAlbilities a) {
        ability = a;
        description.SetText(a.description);

    }
}
