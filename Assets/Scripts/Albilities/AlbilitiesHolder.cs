using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbilitiesHolder : MonoBehaviour
{
    public List<ScriptableAlbilities> list;

    private UnitBase source;

    private void Awake() {
        source = GetComponent<UnitBase>();

        foreach(ScriptableAlbilities a in list) {
            a.AttachTo(source);
        }
    }

    public void AddAbility(ScriptableAlbilities a) {
        Debug.Log(source);
        list.Add(a);
        a.AttachTo(source);
    }
}
