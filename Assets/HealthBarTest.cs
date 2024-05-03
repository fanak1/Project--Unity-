using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarTest : MonoBehaviour
{
    public MoreMountains.Tools.MMHealthBar mmHealthBar;

    public float maxHealth = 100f;
    public float minHealth = 0f;
    public float nowHealth = 0f;
    // Start is called before the first frame update
    void Start()
    {
        mmHealthBar = gameObject.GetComponent<MoreMountains.Tools.MMHealthBar>();
        nowHealth = maxHealth;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (nowHealth > 50f) {
            nowHealth -= Time.deltaTime * 10f;
            mmHealthBar.UpdateBar(nowHealth, minHealth, maxHealth, true);
        }
    }
}
