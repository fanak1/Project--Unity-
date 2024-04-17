using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class SpawnPoint : MonoBehaviour {

    [SerializeField] private float spawnRangeX;
    [SerializeField] private float spawnRangeY;

    private BoxCollider2D boxCollider; //For spawn range

    [SerializeField] private Animator animator;

    private void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        spawnRangeX = boxCollider.bounds.size.x / 2f;
        spawnRangeY = boxCollider.bounds.size.y / 2f;
    }
    public UnitBase SpawnEnemy(ScriptableEnemyUnit enemy) { //Function to spawn 1 enemy with random position in range
        float offsetX = Random.Range(-spawnRangeX, spawnRangeX);
        float offsetY = Random.Range(-spawnRangeY, spawnRangeY);
        Vector3 position = transform.position + Vector3.right * offsetX + Vector3.up * offsetY;
        return enemy.Spawn(position);
    }

    public List<UnitBase> SpawnEnemy(List<ScriptableEnemyUnit> enemy) { //Function to spawn list of enemy with random position in range
        List<UnitBase> list = new List<UnitBase>();
        foreach (ScriptableEnemyUnit e in enemy) {
            list.Add(SpawnEnemy(e));
            
        }
        return list;
    }

    public List<UnitBase> SpawnWithDelay(List<ScriptableEnemyUnit> enemy) {

        return SpawnEnemy(enemy);
    }

    public IEnumerator BeginSpawn() {
        animator.SetTrigger("SpawnBegin");

        yield return new WaitForSeconds(1f);

        animator.SetTrigger("SpawnEnd");
    }

    
}
    



