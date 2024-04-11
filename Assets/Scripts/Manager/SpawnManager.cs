using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField] private float spawnRangeX;
    [SerializeField] private float spawnRangeY;

    private BoxCollider2D boxCollider; //For spawn range

    private void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        spawnRangeX = boxCollider.bounds.size.x;
        spawnRangeY = boxCollider.bounds.size.y;
    }
    public UnitBase Spawn(ScriptableEnemyUnit enemy) { //Function to spawn 1 enemy with random position in range
        float offsetX = Random.Range(-spawnRangeX, spawnRangeX);
        float offsetY = Random.Range(-spawnRangeY, spawnRangeY);
        Vector3 position = transform.position + Vector3.right * offsetX + Vector3.up * offsetY;
        return enemy.Spawn(position);
    }

    public void Spawn(ScriptableEnemyUnit[] enemy) { //Function to spawn list of enemy with random position in range
        foreach(ScriptableEnemyUnit e in enemy) {
            Spawn(e);
        }
    }

}
    



