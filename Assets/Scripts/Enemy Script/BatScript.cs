using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BatScript : MobScript
{

    private Quaternion initialRotation;
    private EnemyBase enemy;

    int moveIndex = 0;

    protected override void Start() {
        base.Start();

        enemy = GetComponent<EnemyBase>();

        initialRotation = transform.rotation;
    }

    

    protected override void UpdateFunction() {
        base.UpdateFunction();
        transform.rotation = initialRotation;
    }

    internal override void Move(int index) {
        
        switch(index) {
            case 0:
                transform.Translate(Vector3.up * enemy.stats.spd * Time.deltaTime);
                break;
            case 1:
                transform.Translate(Vector3.right * enemy.stats.spd * Time.deltaTime);
                break;
            case 2:
                transform.Translate(Vector3.up * -enemy.stats.spd * Time.deltaTime);
                break;
            case 3:
                transform.Translate(Vector3.right * -enemy.stats.spd * Time.deltaTime);
                break;
            case 4:
                MoveToward();
                break;
            default:
                break;
        }
    }

    internal override void RandomMove() {
        if (state == 0) {
            moveIndex = Random.Range(0, 5);
            if (tooFar) moveIndex = 4;
            state = 1;
        } 
        Move(moveIndex);
    }

    protected override void MoveToward() {
        if (player == null) return;
        Vector2 direction = player.transform.position - this.transform.position;
        transform.Translate(direction.normalized * enemy.stats.spd * Time.deltaTime);
    }
}
