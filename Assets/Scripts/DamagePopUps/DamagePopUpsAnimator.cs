using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUpsAnimtor : MonoBehaviour
{
    private RectTransform rectTransform;
    private TMP_Text text;
    private float floatSpeed = 50f;
    private float floatDir;
    private float fadeSpeed = 1f;
    private float lifeTime = 1f;
    private Color originalColor;
    private float ogGravity = 2f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<TMP_Text>();
        originalColor = text.color;
        floatDir = UnityEngine.Random.Range(-1f, 1f);
    }

    void Update()
    {
        ogGravity -= Time.deltaTime * 5f;
        // Move up or down (fall simulation)
        rectTransform.anchoredPosition += Vector2.up * floatSpeed * ogGravity * Time.deltaTime;
        rectTransform.anchoredPosition += Vector2.right * floatDir * floatSpeed * Time.deltaTime;

        // Fade out
        lifeTime -= Time.deltaTime;
        float alpha = Mathf.Clamp01(lifeTime / 1f);
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

        // Destroy when done
        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
