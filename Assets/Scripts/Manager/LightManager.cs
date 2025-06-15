using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : StaticInstance<LightManager>
{
    public Light2D Light;
    private void Start()
    {
        Light = GetComponent<Light2D>();
    }

    public void SetColor(Color32 color)
    {
        Light.color = color;
    }

    public void SetColor(int time)
    {
        Color color = Color.white;
        switch (time)
        {
            case 0:
                color = Color.white;
                break;
            case 1:
                ColorUtility.TryParseHtmlString("#FFD37A", out color);
                break;
            case 2:
                ColorUtility.TryParseHtmlString("#5A6E9C", out color);
                break;
            default:
                break;

        }
        Light.color = color;
    }
}
