using UnityEngine;
using System.Collections.Generic;

public class LightningEffect : MonoBehaviour {
    public Vector3 startPoint;
    public Vector3 endPoint;
    public int segments = 7;
    public float randomness = 0.1f;
    public float lightningDuration = 0.2f;

    private LineRenderer lineRenderer;
    private float timer;

    public static Queue<LightningEffect> lightningEffectPool = new Queue<LightningEffect> ();

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        lineRenderer.positionCount = segments + 1;
        timer = lightningDuration;

        // Ensure it's 2D by setting Z to 0 for all points
        lineRenderer.useWorldSpace = true;
        lineRenderer.startColor = Color.blue; // Set the start color
        lineRenderer.endColor = Color.cyan; // Set the end color for gradient
        lineRenderer.startWidth = 0.05f; // Set start width
        lineRenderer.endWidth = 0.05f; // Set end width

        CreateLightning();
        
    }

    void CreateLightning() {
        Vector3 start = startPoint;
        Vector3 end = endPoint;

        for (int i = 0; i <= segments; i++) {
            float t = i / (float)segments;
            Vector3 position = Vector3.Lerp(start, end, t);

            // Apply random offsets only in the X and Y axes
            position.x += Random.Range(-randomness, randomness);
            position.y += Random.Range(-randomness, randomness);
            position.z = 0;  // Keep Z at 0 for 2D effect

            lineRenderer.SetPosition(i, position);
        }
        Invoke("ReturnToPool", lightningDuration);
    }

    private void OnEnable() {
        if(lineRenderer != null) 
        CreateLightning();
    }



    private void Draw(Vector3 start, Vector3 end) {
        this.startPoint = start;
        this.endPoint = end;
        
    }

    private void ReturnToPool() {
        gameObject.SetActive(false);
        lightningEffectPool.Enqueue(this);
    }

    public static LightningEffect Create(Vector3 start, Vector3 end) {
        LightningEffect lightningEffect;
        if(lightningEffectPool.Count <= 0) {
            GameObject go = new GameObject();
            go.AddComponent<LineRenderer>();
            lightningEffect = go.AddComponent<LightningEffect>();
        } else {
            lightningEffect = lightningEffectPool.Dequeue();
        }
        lightningEffect.Draw(start, end);
        lightningEffect.transform.gameObject.SetActive(true);
        return lightningEffect;

    }
}