using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingRoomManager : Singleton<TrainingRoomManager>
{

    private List<ScriptableAlbilities> allAbilities;


    // Start is called before the first frame update
    void Start()
    {
        allAbilities = ResourceSystem.Instance.GetAllAbilities();
    }

    public void AddAbility(ScriptableAlbilities ability) {
        PlayerUnit.instance.AddAbility(ability);
    }


    private void Init() {
        
    }

}
