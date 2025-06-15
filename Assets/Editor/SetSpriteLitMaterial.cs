using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using UnityEditor.SceneManagement;

public class SetSpriteLitMaterial : MonoBehaviour
{
    private const string litMaterialPath = "Packages/com.unity.render-pipelines.universal/Runtime/Materials/Sprite-Lit-Default.mat";

    [MenuItem("Tools/Convert Sprite & Tilemap Renderers to Built-In Sprite-Lit (Scene + Prefab Edit)")]
    public static void ConvertRenderers()
    {
        Material litMaterial = AssetDatabase.LoadAssetAtPath<Material>(litMaterialPath);
        if (litMaterial == null)
        {
            Debug.LogError($"Could not load built-in Sprite-Lit-Default material at:\n{litMaterialPath}");
            return;
        }

        int sceneSpriteCount = 0;
        int sceneTilemapCount = 0;
        int prefabSpriteCount = 0;
        int prefabTilemapCount = 0;

        // 🎮 Convert all objects in the active scene
        foreach (var sr in Object.FindObjectsByType<SpriteRenderer>(FindObjectsSortMode.None))
        {
            if (sr.sharedMaterial != litMaterial)
            {
                Undo.RecordObject(sr, "Assign Sprite-Lit Material");
                sr.sharedMaterial = litMaterial;
                sceneSpriteCount++;
            }
        }

        foreach (var tr in Object.FindObjectsByType<TilemapRenderer>(FindObjectsSortMode.None))
        {
            if (tr.sharedMaterial != litMaterial)
            {
                Undo.RecordObject(tr, "Assign Sprite-Lit Material");
                tr.sharedMaterial = litMaterial;
                sceneTilemapCount++;
            }
        }

        // 🧱 Convert objects in open prefab editing mode (if any)
        var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        if (prefabStage != null)
        {
            GameObject root = prefabStage.prefabContentsRoot;

            foreach (var sr in root.GetComponentsInChildren<SpriteRenderer>(true))
            {
                if (sr.sharedMaterial != litMaterial)
                {
                    Undo.RecordObject(sr, "Assign Sprite-Lit Material");
                    sr.sharedMaterial = litMaterial;
                    prefabSpriteCount++;
                }
            }

            foreach (var tr in root.GetComponentsInChildren<TilemapRenderer>(true))
            {
                if (tr.sharedMaterial != litMaterial)
                {
                    Undo.RecordObject(tr, "Assign Sprite-Lit Material");
                    tr.sharedMaterial = litMaterial;
                    prefabTilemapCount++;
                }
            }
        }

        Debug.Log($" Updated materials to Sprite-Lit-Default:\n" +
                  $"- Scene: {sceneSpriteCount} SpriteRenderer(s), {sceneTilemapCount} TilemapRenderer(s)\n" +
                  $"- Prefab Edit: {prefabSpriteCount} SpriteRenderer(s), {prefabTilemapCount} TilemapRenderer(s)");
    }
}