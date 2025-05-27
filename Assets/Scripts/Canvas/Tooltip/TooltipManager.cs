using TMPro;
using UnityEngine;

public class TooltipManager : Singleton<TooltipManager>
{

    public GameObject tooltipObject;
    public TextMeshProUGUI tooltipText;
    public Vector2 offset;

    private RectTransform tooltipRect;
    private bool isTooltipActive = false;

    void Start()
    {
        tooltipRect = tooltipObject.GetComponent<RectTransform>();
        tooltipObject.SetActive(false);
        offset = new Vector2(15, -15);
    }

    public void Show(string text)
    {
        tooltipText.text = text;
        tooltipObject.SetActive(true);
        isTooltipActive = true;
        UpdatePosition();
    }

    public void Hide()
    {
        tooltipObject.SetActive(false);
        isTooltipActive = false;
    }

    void Update()
    {
        if (isTooltipActive)
        {
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        Vector2 mousePos = Input.mousePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            tooltipRect.parent as RectTransform,
            mousePos,
            null,
            out Vector2 localPoint
        );

        tooltipRect.anchoredPosition = localPoint + offset;
    }
}