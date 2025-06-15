using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class AddUIButtonSound : MonoBehaviour
{
    [MenuItem("Tools/Add Sound Script to All Buttons in Scene")]
    public static void AddSoundScriptToButtons()
    {
        var buttons = Object.FindObjectsByType<Button>(FindObjectsSortMode.None);
        int count = 0;

        foreach (var button in buttons)
        {
            if (button.GetComponent<UIButtonSound>() == null)
            {
                Undo.AddComponent<UIButtonSound>(button.gameObject);
                count++;
            }
        }

        Debug.Log($"Added UIButtonSound to {count} Button(s) in the scene.");
    }
}
