using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHolder : MonoBehaviour
{
    [SerializeField]
    private List<ScriptableProjectiles> projectileList; //List of this object's projectile

    private List<Projectiles> projectiles;

    private UnitBase source; //Source of this ProjectileHolder

    private float time;

    private void Awake() {
        source = GetComponent<UnitBase>();
    }

    private void Start() {
        projectiles = new List<Projectiles>();
        foreach(var projectile in projectileList) {
            var p = projectile.Create();
            projectiles.Add(p);
        }
        foreach(var p in projectiles) {
            p.Init(source);
        }
    }

    private void Update() {
        if (time > 50f) time = 40f;
        time += Time.deltaTime;
    }

    public void Shoot(int projectileIndex, Vector3 position, Vector3 destination) { //Funciton to Shoot
        if(ShootInterval(projectileIndex)) {
            projectiles[projectileIndex].Shoot(source, position, destination);
            time = 0f;
            source.ReduceMP(MPCost(0));
        }
        
    }

    public void RandomShoot(Vector3 position, Vector3 destination) { 
        if (projectileList.Count > 0) {
            int projectileIndex = Random.Range(0, projectileList.Count);
            Shoot(projectileIndex, position, destination);
        }
    }

    public void AddProjectile(ScriptableProjectiles projectile) { //Add a projectile to list
        projectileList.Add(projectile);
    }

    private bool ShootInterval(int projectileIndex) { //Interval of each shoot
        return projectiles[projectileIndex].ShootInterval(time);
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
            projectiles[index].SetStatForProjectile(projectileAttribute);
        }
    }

    public bool EnoughMana(int index, float mana) {
        if (mana < projectiles[index].GetAttribute().manaSpend) return false;
        return true;
    }

    public float MPCost(int index) {
        return projectiles[index].GetAttribute().manaSpend;
    }

}
