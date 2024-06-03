using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : MobScript
{
    int moveIndex = 0;
    internal override void Move(int index) {
        switch(index) {
            case 0:
                transform.Translate(Vector3.up * 1 * spd * Time.deltaTime);
                break;
            case 1:
                transform.Translate(Vector3.right * 1 * spd * Time.deltaTime);
                break;
            case 3:
                transform.Translate(Vector3.up * -1 * spd * Time.deltaTime);
                break;
            case 4:
                transform.Translate(Vector3.right * -1 * spd * Time.deltaTime);
                break;
            case 5:
                base.MoveToward();
                break;
            default:
                break;
        }
    }

    internal override void RandomMove() {
        if (state == 0) {
            moveIndex = Random.Range(0, 5);
            if (tooFar) moveIndex = 5;
            state = 1;
        } 
        Move(moveIndex);
    }
}
