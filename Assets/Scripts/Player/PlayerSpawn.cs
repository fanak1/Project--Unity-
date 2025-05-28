using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : Singleton<PlayerSpawn>
{

    [SerializeField] public ScriptablePlayerUnit scriptablePlayer;
    // Start is called before the first frame update
    public PlayerData data;

    public void Spawn()
    {
        UnitBase player;
        if(data.allAlbilities != null)
        {
            var list = ResourceSystem.Instance.GetListAlbilities(data.allAlbilities);
            player = scriptablePlayer.Spawn(transform.position, list);
            player.stats = data.stats;
        } else {
            player = scriptablePlayer.Spawn(transform.position);
        }

        player.gameObject.transform.SetParent(this.transform);
    }
}
