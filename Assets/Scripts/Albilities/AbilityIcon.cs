using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour, ITooltip
{
    [SerializeField] ScriptableAlbilities ability;

    public Image image;

    public Image filter;

    private byte alphaFilter = 40;

    void Start()
    {
        if(ability != null)
        {
            SetupUI(ability);
        }
    }

    public void Init(ScriptableAlbilities a)
    {
        this.ability = a;
        SetupUI(a);
    }

    public void SetupUI(ScriptableAlbilities a)
    {
        if(a.icon != null)
        {
            image.sprite = a.icon;
            image.color = Color.white;
            
            var colorFilter = Registry.AbilityRarityColor(a.rarity);
            filter.color = new Color32(
                (byte)(colorFilter.r * 255),
                (byte)(colorFilter.g * 255),
                (byte)(colorFilter.b * 255),
                alphaFilter
            );
        }
        else
        {
            image.color = Color.gray;
            filter.color = new Color32(0, 0, 0, alphaFilter);
        }
    }


    public string ToolTipText()
    {
        return this.ability.description;
    }
}
