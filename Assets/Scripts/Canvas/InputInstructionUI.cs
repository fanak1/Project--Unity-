using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputInstructionUI : StaticInstance<InputInstructionUI>
{
    public InputInstruction prefab;

    public Transform inputInstructions;

    List<KeyValuePair<string, List<string>>> instructions = new();

    List<KeyValuePair<string, GameObject>> instructionsObj = new();

    public void Start()
    {
        AddInstructions(new KeyValuePair<string, List<string>>("Mouse", new List<string>(){"W", "A" }));
    }

    public void AddInstructions(KeyValuePair<string, List<string>> instruction)
    {
        var obj = Instantiate(prefab);
        obj.transform.SetParent(inputInstructions);
        obj.Init(instruction.Key, instruction.Value);
        this.instructions.Add(instruction);
        instructionsObj.Add(new KeyValuePair<string, GameObject>(instruction.Key, obj.gameObject));
    }

    public void RemoveInstructions(string instrucionText)
    {
        foreach (var i in this.instructionsObj) {
            if (i.Key == instrucionText)
            {
                var obj = i.Value;
                Destroy(obj);
            }
        }

        instructions.RemoveAll(i => i.Key == instrucionText);
        instructionsObj.RemoveAll(i => { return i.Key == instrucionText || i.Value == null;});
    }
}
