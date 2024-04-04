using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHolder : MonoBehaviour
{
    [SerializeField]
    private List<ScriptableProjectiles> projectileList; //List of this object's projectile

    private UnitBase source; //Source of this ProjectileHolder

    private void Start() {
        source = GetComponent<UnitBase>();
    }

    public void Shoot(int projectileIndex, Vector3 position, Vector3 destination) { //Funciton to Shoot
        projectileList[projectileIndex].Shoot(source, position, destination);
    }

    public void AddProjectile(ScriptableProjectiles projectile) { //Add a projectile to list
        projectileList.Add(projectile);
    }

}
