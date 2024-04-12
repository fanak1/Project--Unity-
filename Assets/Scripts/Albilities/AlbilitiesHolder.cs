using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbilitiesHolder : MonoBehaviour
{
    public List<ScriptableAlbilities> list;

    private UnitBase source;

    private void Start() {
        source = GetComponent<UnitBase>();

        foreach(ScriptableAlbilities a in list) {
            a.AttachTo(source);
        }
    }

    public void AddAbility(ScriptableAlbilities a) {
        list.Add(a);
        a.AttachTo(source);
    }
}
