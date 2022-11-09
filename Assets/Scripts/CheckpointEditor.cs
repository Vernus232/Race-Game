using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.EditorTools;

[EditorTool("Checkpoint Editor",typeof(Checkpoint))]
public class CheckpointEditor : EditorTool
{
    public Texture2D toolIcon;
    public override GUIContent toolbarIcon
    {
        get
        {
            return new GUIContent
            {
                image = toolIcon,
                text = "RaceEditor",
                tooltip = "Create races)"
            };
        }
    }

    public override void OnToolGUI(EditorWindow window)
    {
        Transform targetTransform = ((Checkpoint) target).transform;

        EditorGUI.BeginChangeCheck();
        Vector3 newPosition = Handles.PositionHandle(targetTransform.position, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            targetTransform.position = newPosition;
        }
    }
}