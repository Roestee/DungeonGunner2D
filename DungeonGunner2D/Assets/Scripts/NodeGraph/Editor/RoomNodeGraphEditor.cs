using UnityEditor;
using UnityEngine;

namespace NodeGraph.Editor
{
    public class RoomNodeGraphEditor : EditorWindow
    {
        private GUIStyle _roomNodeStyle;
        
        //Node layout values
        private const float NodeWidth = 160f, NodeHeight = 75f;
        private const int NodePadding = 25, NodeBorder = 12;
        
        [MenuItem("Room Node Graph Editor", menuItem = "Window/Dungeon Editor/Room Node Graph Editor")]
        private static void OpenWindow()
        {
            GetWindow<RoomNodeGraphEditor>("Room Node Graph Editor");
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(100, 100, NodeWidth, NodeHeight), _roomNodeStyle);
            EditorGUILayout.LabelField("Node 1");
            GUILayout.EndArea();
            
            GUILayout.BeginArea(new Rect(300, 300, NodeWidth, NodeHeight), _roomNodeStyle);
            EditorGUILayout.LabelField("Node 2");
            GUILayout.EndArea();
        }

        private void OnEnable()
        {
            _roomNodeStyle = new GUIStyle();
            _roomNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            _roomNodeStyle.normal.textColor = Color.white;
            _roomNodeStyle.padding = new RectOffset(NodePadding, NodePadding, NodePadding, NodePadding);
            _roomNodeStyle.border = new RectOffset(NodeBorder, NodeBorder, NodeBorder, NodeBorder);
        }
    }
}

