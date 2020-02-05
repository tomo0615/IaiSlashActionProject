using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

#if UNITY_EDITOR
[CustomEditor(typeof(EffectsTable))]
public class EffectsTableCreator : Editor
{
    const string effectEnumName = "EffectType";

    public override void OnInspectorGUI()
    {
        var effectTable = target as EffectsTable;

        DrawDefaultInspector();

        if (GUILayout.Button("Create EffectType"))
        {
            ///Debug.Log("Success Enum EffectType");     
            CreateEffectType(effectTable);
        }
    }

    public static void CreateEffectType(EffectsTable effectTable)
    {
        List<string> names = new List<string>();

        foreach (var effect in effectTable.gameEffectList)
        {
            names.Add(effect.name);
        }

        //Enum作成
        EnumCreator.Create(
            enumName: effectEnumName,          //enumの名前
            itemNameList: names,                //enumの項目
                                                //作成したファイルのパスをAssetsから拡張子まで指定
            exportPath: "Assets/GameEffectManager/Scripts/" + effectEnumName + ".cs"
        );
    }
}
#endif