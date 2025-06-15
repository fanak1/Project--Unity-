using UnityEngine;

public class Expandable : MonoBehaviour
{
    public int state = 0;

    public RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Click()
    {
        switch(state)
        {
            case 0:
                Expand();
                break;
            case 1:
                Close();
                break;
            default:
                break;
        }
    }
    public void Expand()
    {
        state = 1;
        Vector2 size = rectTransform.sizeDelta;
        size.y = 300f;
        rectTransform.sizeDelta = size;
    }

    public void Close()
    {
        state = 0;
        Vector2 size = rectTransform.sizeDelta;
        size.y = 100f;
        rectTransform.sizeDelta = size;
    }
}
