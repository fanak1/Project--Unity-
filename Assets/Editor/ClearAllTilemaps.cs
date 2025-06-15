using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class ClearAllTilemaps : MonoBehaviour
{
    [MenuItem("Tools/Clear All Tiles in Scene")]
    public static void ClearTilesInAllTilemaps()
    {
        Tilemap[] tilemaps = Object.FindObjectsByType<Tilemap>(FindObjectsSortMode.None);

        int count = 0;

        foreach (var tilemap in tilemaps)
        {
            if (tilemap != null && tilemap.cellBounds.size != Vector3Int.zero)
            {
                Undo.RecordObject(tilemap, "Clear Tilemap");
                tilemap.ClearAllTiles();
                count++;
            }
        }

        Debug.Log($"Cleared {count} tilemap(s) in the scene.");
    }
}
