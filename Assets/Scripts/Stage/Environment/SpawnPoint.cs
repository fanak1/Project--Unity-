using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class SpawnPoint : MonoBehaviour {

    public float spawnRangeX = 1f;
    public float spawnRangeY = 1f;

    private List<ScriptableEnemyUnit> listEnemy;

    private BoxCollider2D boxCollider; //For spawn range

    [SerializeField] private Animator animator;

    private void Start() {
        //transform.localScale = new Vector3(spawnRangeX * 2f, spawnRangeY * 2f, 1);

        
        boxCollider = GetComponent<BoxCollider2D>();
        spawnRangeX = boxCollider.size.x;
        spawnRangeY = boxCollider.size.y;

        //StartCoroutine("BeginSpawn");
    }
    public UnitBase SpawnEnemy(ScriptableEnemyUnit enemy) { //Function to spawn 1 enemy with random position in range
        float offsetX = Random.Range(-spawnRangeX, spawnRangeX);
        float offsetY = Random.Range(-spawnRangeY, spawnRangeY);
        Vector3 position = transform.position + Vector3.right * offsetX + Vector3.up * offsetY;
        return enemy.Spawn(position);
    }

    public List<UnitBase> SpawnEnemy(List<ScriptableEnemyUnit> enemy, int level = 1) { //Function to spawn list of enemy with random position in range
        
        List<UnitBase> list = new List<UnitBase>();
        int i = 0;
        foreach (ScriptableEnemyUnit e in enemy) {
            var go = SpawnEnemy(e);
            go.SetLevel(level);
            go.transform.name = e.name + " " + i;
            list.Add(go);

            i++;
        }
        return list;
    }

    public List<UnitBase> SpawnWithDelay(List<ScriptableEnemyUnit> enemy) {

        return SpawnEnemy(enemy);
    }



    public void SetSpawnRange(Vector3 spawnRange) {
        this.spawnRangeX = Mathf.Abs(spawnRange.x) / 2f;
        this.spawnRangeY = Mathf.Abs(spawnRange.y) / 2f;
    }

    public void SetEnemy(List<ScriptableEnemyUnit> enemyUnits) {
        this.listEnemy = new List<ScriptableEnemyUnit>();
        this.listEnemy.Clear();
        this.listEnemy.AddRange(enemyUnits);
    }

    public void Spawn(int level = 1) {
        if(listEnemy != null)
            CoroutineManager.Instance.StartNewCoroutine(BeginSpawn(level));
    }

    public IEnumerator BeginSpawn(int level = 1) {
        animator.SetTrigger("SpawnBegin");

        yield return new WaitForSeconds(1f);

        animator.SetTrigger("SpawnEnd");

        SpawnEnemy(this.listEnemy, level);
    }

    
}
    



