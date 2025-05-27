using UnityEngine;
using UnityEditor;
using TMPro;

public class ReplaceTMPFont : EditorWindow
{
    TMP_FontAsset newFont;

    [MenuItem("Tools/Replace TMP Font")]
    public static void ShowWindow()
    {
        GetWindow<ReplaceTMPFont>("Replace TMP Font");
    }

    void OnGUI()
    {
        GUILayout.Label("Replace TMP Font", EditorStyles.boldLabel);
        newFont = (TMP_FontAsset)EditorGUILayout.ObjectField("New Font Asset", newFont, typeof(TMP_FontAsset), false);

        if (GUILayout.Button("Replace All in Scene"))
        {
            if (newFont == null)
            {
                Debug.LogWarning("No TMP_FontAsset selected.");
                return;
            }

            ReplaceAllTMPFontsInScene();
        }
    }

    void ReplaceAllTMPFontsInScene()
    {
        var textComponents = FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None);
        // include inactive
        int count = 0;

        foreach (var tmp in textComponents)
        {
            Undo.RecordObject(tmp, "Replace TMP Font");
            tmp.font = newFont;
            EditorUtility.SetDirty(tmp);
            count++;
        }

        Debug.Log($"Replaced font on {count} TextMeshProUGUI components.");
    }
}
