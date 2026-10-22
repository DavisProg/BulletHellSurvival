using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player))]
public class levelRequirementManager : Editor
{
    private float percentage = 1f;
    private int startLevel = 1;

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
        EditorGUILayout.LabelField("Starting from level ", EditorStyles.boldLabel);

        startLevel = EditorGUILayout.IntField(
            "level",
            startLevel
        );

        if (GUILayout.Button("Increase All Values"))
        {
            Undo.RecordObject(player, "Increase All Values");

            player.increaseXPRequirement(percentage, startLevel);

            EditorUtility.SetDirty(player);
            PrefabUtility.RecordPrefabInstancePropertyModifications(player);
        }
    }
}
