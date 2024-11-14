using UnityEngine;
using UnityEditor;

public class generatePrefabs : MonoBehaviour
{
    [MenuItem("Tools/Generate Prefabs from Tiles")]
    static void GeneratePrefabs()
    {
        // Load the sliced sprites from the tileset
        string spriteSheetPath = "Assets/Asset_gambar/maptanah.png"; // Modify this path
        Object[] sprites = AssetDatabase.LoadAllAssetsAtPath(spriteSheetPath);

        // Folder to store the prefabs
        string prefabFolder = "Assets/Prefabs/PUNY"; // Modify this path
        if (!AssetDatabase.IsValidFolder(prefabFolder))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }

        foreach (Object obj in sprites)
        {
            if (obj is Sprite)
            {
                Sprite sprite = (Sprite)obj;
                GameObject newTile = new GameObject(sprite.name);
                SpriteRenderer sr = newTile.AddComponent<SpriteRenderer>();
                sr.sprite = sprite;

                // Save the GameObject as a prefab
                string prefabPath = prefabFolder + "/" + sprite.name + ".prefab";
                PrefabUtility.SaveAsPrefabAsset(newTile, prefabPath);

                // Destroy the temporary GameObject
                DestroyImmediate(newTile);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Prefabs generated!");
    }
}
