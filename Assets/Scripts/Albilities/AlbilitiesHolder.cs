using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbilitiesHolder : MonoBehaviour
{
    public List<ScriptableAlbilities> list;

    public List<ScriptableAlbilities> listSkill;

    private UnitBase source;

    private void Awake() {
        source = GetComponent<UnitBase>();

        foreach(ScriptableAlbilities a in list) {
            a.AttachTo(source);
        }
    }

    public void AddAbility(ScriptableAlbilities a) {
        //Debug.Log(source);
        if(a.onEvent != Event.OnButtonClick) {
            list.Add(a);
            
        } else {
            listSkill.Add(a);
        }
        a.AttachTo(source);
    }
}
