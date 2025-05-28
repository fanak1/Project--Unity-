using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour, ITooltip
{
    [SerializeField] ScriptableAlbilities ability;

    public Image image;

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
        }
        else
        {
            image.color = Color.gray;
        }
    }


    public string ToolTipText()
    {
        return this.ability.description;
    }
}
