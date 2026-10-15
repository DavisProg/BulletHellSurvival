using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player))]
public class levelRequirementManager : Editor
{
    private float percentage = 1f;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Player player = (Player)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Change value to %", EditorStyles.boldLabel);

        percentage = EditorGUILayout.FloatField(
            "Percentage",
            percentage
        );

        if (GUILayout.Button("Increase All Values"))
        {
            Undo.RecordObject(player, "Increase All Values");

            player.increaseXPRequirement(percentage);

            EditorUtility.SetDirty(player);
            PrefabUtility.RecordPrefabInstancePropertyModifications(player);
        }
    }
}
