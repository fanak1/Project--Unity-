using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{

    [SerializeField] private ScriptablePlayerUnit scriptablePlayer;
    // Start is called before the first frame update
    void Start()
    {
        var player = scriptablePlayer.Spawn(transform.position);
        player.gameObject.transform.SetParent(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
