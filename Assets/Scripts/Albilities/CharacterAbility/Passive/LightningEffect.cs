using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class LightningEffect : RenderSpriteBetweenTwoPoint {

    public static Queue<LightningEffect> pool = new Queue<LightningEffect>();

    public static GameObject prefabs;

    static LightningEffect()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        pool.Clear();
    }

    protected override void Start() {
        base.Start();
    }

    protected virtual void OnEnable() {

    }

    public virtual void Destroy(UnitBase a) {
        gameObject.SetActive(false);
        pool.Enqueue(this);
    }
    public static LightningEffect Create() {
        LightningEffect effect;
        if(prefabs == null) {
            prefabs = Resources.Load<GameObject>("EffectPrefabs/LightningRay");
            Debug.Log(prefabs);
        }
        if(pool.Count <= 0) {
            var go = Instantiate(prefabs);
            effect = go.GetComponent<LightningEffect>();
            
        } else {
            effect = pool.Dequeue();
            effect.gameObject.SetActive(true);
        }
        effect.OnEffectHit = null;
        effect.OnEffectHit += effect.Destroy;
        return effect;
    }
}