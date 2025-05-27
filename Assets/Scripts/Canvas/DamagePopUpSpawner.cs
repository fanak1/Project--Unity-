using TMPro;
using UnityEngine;

public class DamagePopUpSpawner : Singleton<DamagePopUpSpawner>
{
    public GameObject dmgPopUpPrefab; // Assign a UI prefab (Text or TMP)
    public Canvas uiCanvas;           // Screen Space - Overlay Canvas

    public void ShowDamage(Vector3 worldPosition, int damageAmount, bool critted)
    {
        // Convert world position to screen position
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

        // Add some random offset
        float offsetX = Random.Range(-50f, 50f);
        float offsetY = Random.Range(30f, 60f);
        Vector2 randomOffset = new Vector2(offsetX, offsetY);
        Vector2 finalScreenPos = screenPos + randomOffset;

        // Instantiate the popup
        GameObject popUp = Instantiate(dmgPopUpPrefab, uiCanvas.transform);

        // Set position
        RectTransform canvasRect = uiCanvas.GetComponent<RectTransform>();
        RectTransform rectTransform = popUp.GetComponent<RectTransform>();
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect, finalScreenPos,
            uiCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : uiCanvas.worldCamera,
            out localPoint);
        rectTransform.anchoredPosition = localPoint;

        // Set text
        TextMeshProUGUI text = popUp.GetComponent<TextMeshProUGUI>();
        text.text = damageAmount.ToString();
        text.color = critted ? Color.red : Color.yellow;
        text.fontSize = critted ? 50 : 40;

        // Start animation
        popUp.AddComponent<DamagePopUpsAnimtor>();
    }
}