using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ITooltip tooltipText;
    void Start()
    {
        tooltipText = GetComponent<ITooltip>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(tooltipText != null) 
            TooltipManager.Instance.Show(tooltipText.ToolTipText());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.Hide();
    }
}
