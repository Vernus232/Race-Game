using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.EditorTools;

[EditorTool("Race Editor",typeof(Checkpoint))]
public class RaceSystem : EditorTool
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