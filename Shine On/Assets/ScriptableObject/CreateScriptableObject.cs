using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeScriptableObject
{
    [MenuItem("Assets/Create/New Plant")]
    public static void CreateMyAsset()
    {
        Plant asset = ScriptableObject.CreateInstance<Plant>();

        AssetDatabase.CreateAsset(asset, "Assets/NewPlant.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}