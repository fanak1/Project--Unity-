using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHolder : MonoBehaviour
{
    [SerializeField]
    private List<ScriptableProjectiles> projectileList; //List of this object's projectile

    private UnitBase source; //Source of this ProjectileHolder

    private float time;

    private void Start() {
        source = GetComponent<UnitBase>();
    }

    private void Update() {
        if (time > 50f) time = 40f;
        time += Time.deltaTime;
    }

    public void Shoot(int projectileIndex, Vector3 position, Vector3 destination) { //Funciton to Shoot
        if(ShootInterval(projectileIndex)) {
            projectileList[projectileIndex].Shoot(source, position, destination);
            time = 0f;
        }
        
    }

    public void AddProjectile(ScriptableProjectiles projectile) { //Add a projectile to list
        projectileList.Add(projectile);
    }

    private bool ShootInterval(int projectileIndex) { //Interval of each shoot
        return projectileList[projectileIndex].ShootInterval(time);
    }

    public void ModifyProjectile(ProjectileCodeName codeName, ProjectileAttribute projectileAttribute) {// Maybe only use for Player xD
        int index = -1;
        for(int i = 0; i<projectileList.Count; i++) {
            if (projectileList[i].projectileCodeName == codeName) {
                index = i;
                break;
            }
        }
        if(index >=0) {
            projectileList[index].SetStatForProjectile(projectileAttribute);
        }
    }

}
