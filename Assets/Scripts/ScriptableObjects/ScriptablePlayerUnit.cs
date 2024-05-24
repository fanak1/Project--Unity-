using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Player")]
public class ScriptablePlayerUnit : ScriptableEntity
{
    public override UnitBase Spawn(Vector3 position) {
        base.InitUnit();
        var obj = Instantiate(prefabs, position, Quaternion.identity);
        obj.OnBaseStatsIncrease += BaseStatsChange;
        return obj;
    }
}
