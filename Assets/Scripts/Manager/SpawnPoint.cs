using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class SpawnPoint : MonoBehaviour {

    public float spawnRangeX;
    public float spawnRangeY;

    private List<ScriptableEnemyUnit> listEnemy;

    private BoxCollider2D boxCollider; //For spawn range

    [SerializeField] private Animator animator;

    private void Start() {
        transform.localScale = new Vector3(spawnRangeX, spawnRangeY, 1);
        //boxCollider = GetComponent<BoxCollider2D>();
        //spawnRangeX = boxCollider.bounds.size.x / 2f;
        //spawnRangeY = boxCollider.bounds.size.y / 2f;
        StartCoroutine("BeginSpawn");
    }
    public UnitBase SpawnEnemy(ScriptableEnemyUnit enemy) { //Function to spawn 1 enemy with random position in range
        float offsetX = Random.Range(-spawnRangeX, spawnRangeX);
        float offsetY = Random.Range(-spawnRangeY, spawnRangeY);
        Vector3 position = transform.position + Vector3.right * offsetX + Vector3.up * offsetY;
        return enemy.Spawn(position);
    }

    public List<UnitBase> SpawnEnemy(List<ScriptableEnemyUnit> enemy) { //Function to spawn list of enemy with random position in range
        Debug.Log(enemy.Count);
        List<UnitBase> list = new List<UnitBase>();
        foreach (ScriptableEnemyUnit e in enemy) {
            list.Add(SpawnEnemy(e));
            
        }
        return list;
    }

    public List<UnitBase> SpawnWithDelay(List<ScriptableEnemyUnit> enemy) {

        return SpawnEnemy(enemy);
    }



    public void SetSpawnRange(Vector3 spawnRange) {
        this.spawnRangeX = Mathf.Abs(spawnRange.x);
        this.spawnRangeY = Mathf.Abs(spawnRange.y);
    }

    public void SetEnemy(List<ScriptableEnemyUnit> enemyUnits) {
        this.listEnemy = new List<ScriptableEnemyUnit>();
        this.listEnemy.Clear();
        this.listEnemy.AddRange(enemyUnits);
        
    }

    public IEnumerator BeginSpawn() {
        animator.SetTrigger("SpawnBegin");

        yield return new WaitForSeconds(1f);

        animator.SetTrigger("SpawnEnd");

        SpawnEnemy(this.listEnemy);
    }

    
}
    


