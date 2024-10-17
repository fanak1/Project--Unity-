using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbilitiesHolder : MonoBehaviour
{
    public List<ScriptableAlbilities> list;

    public List<ScriptableAlbilities> listSkill;

    public List<Abilities> passive;

    public List<Abilities> active;

    private UnitBase source;

    private void Awake() {
        source = GetComponent<UnitBase>();

        foreach(ScriptableAlbilities a in list) {
            var ability = a.Create();
            if(a.onEvent != Event.OnButtonClick) {
                passive.Add(ability);
            } else {
                active.Add(ability);
            }
            ability.AttachTo(source);
        }
    }

    public void AddAbility(ScriptableAlbilities a) {
        //Debug.Log(source);
        var ability = a.Create();
        if(a.onEvent != Event.OnButtonClick) {
            list.Add(a);
            passive.Add(ability);
        } else {
            listSkill.Add(a);
            active.Add(ability);
        }
        ability.AttachTo(source);
    }
}
