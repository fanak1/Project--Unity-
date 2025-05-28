using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Player")]
public class ScriptablePlayerUnit : ScriptableEntity
{
    public override UnitBase Spawn(Vector3 position, List<ScriptableAlbilities> abilities = null, List<ScriptableProjectiles> projectiles = null) {
        UnitBase obj = base.Spawn(position, abilities, projectiles);
        obj.OnBaseStatsIncrease += BaseStatsChange;
        return obj;
    }
}
