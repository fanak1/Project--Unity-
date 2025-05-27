using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class InputInstruction : MonoBehaviour
{
    public TextMeshProUGUI text;

    public GameObject iconPrefabs;

    public Transform icons;

    private void Start()
    {
        
    }

    public void Init(string text, List<string> inputs)
    {
        foreach (string input in inputs)
        {
            Sprite sprite = Resources.Load<Sprite>($"InputInstructions/{input}");
            var obj = Instantiate(iconPrefabs);
            obj.name = input;
            obj.transform.SetParent(icons);
            obj.GetComponent<Image>().sprite = sprite;
        }

        this.text.SetText(text);
    }

}
