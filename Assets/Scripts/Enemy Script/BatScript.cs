using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BatScript : MobScript
{

    private Quaternion initialRotation;
    private NavMeshAgent agent;

    int moveIndex = 0;

    protected override void Start() {
        base.Start();

        agent = GetComponent<NavMeshAgent>();

        agent.speed = spd;

        initialRotation = transform.rotation;
    }

    

    protected override void Update() {
        base.Update();
        transform.rotation = initialRotation;
    }

    internal override void Move(int index) {
        
        switch(index) {
            case 0:
                agent.SetDestination(transform.position + Vector3.up);
                break;
            case 1:
                agent.SetDestination(transform.position + Vector3.right);
                break;
            case 2:
                agent.SetDestination(transform.position + Vector3.up * -1);
                break;
            case 3:
                agent.SetDestination(transform.position + Vector3.right * -1);
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
        if (player != null) {
            agent.SetDestination(Vector3.Lerp((Vector2)this.transform.position, (Vector2)player.transform.position, spd / (Vector3.Distance(this.transform.position, player.transform.position) + 5f)));
        }
    }
}
