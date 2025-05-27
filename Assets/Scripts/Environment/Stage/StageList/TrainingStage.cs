using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingStage : StageScript
{
    public override void StageStart()
    {
        CloseDoor(() =>
        {
            StageManager.Instance.StartStage(this);
        });
    }
}
