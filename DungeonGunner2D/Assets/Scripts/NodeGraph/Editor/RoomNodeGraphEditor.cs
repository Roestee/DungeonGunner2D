using GameManager;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace NodeGraph.Editor
{
    public class RoomNodeGraphEditor : EditorWindow
    {
        private GUIStyle _roomNodeStyle;
        private RoomNodeTypeListSO _roomNodeTypeList;
        private static RoomNodeGraphSO _currentRoomNodeGraph;
        
        //Node layout values
        private const float NodeWidth = 160f, NodeHeight = 75f;
        private const int NodePadding = 25, NodeBorder = 12;
        
        [MenuItem("Room Node Graph Editor", menuItem = "Window/Dungeon Editor/Room Node Graph Editor")]
        private static void OpenWindow()
        {
            GetWindow<RoomNodeGraphEditor>("Room Node Graph Editor");
        }

        /// <summary>
        /// Open the room node graph editor window if a room node graph scriptable object asset is double clicked in the inspector
        /// </summary>
        [OnOpenAsset(0)]
        public static bool OnDoubleClickAsset(int instanceID, int line)
        {
            var roomNodeGraph = EditorUtility.InstanceIDToObject(instanceID) as RoomNodeGraphSO;
            if (roomNodeGraph == null) 
                return false;
            
            OpenWindow();
            _currentRoomNodeGraph = roomNodeGraph;
            
            return true;
        }

        private void OnGUI()
        {
            if (_currentRoomNodeGraph != null)
            {
                ProcessEvents(Event.current);
                DrawRoomNodes();
            }
            
            if(GUI.changed)
                Repaint();
        }

        private void ProcessEvents(Event currentEvent)
        {
            ProcessRoomNodeGraphEvents(currentEvent);
        }

        private void ProcessRoomNodeGraphEvents(Event currentEvent)
        {
            switch (currentEvent.type)
            {
                case EventType.MouseDown:
                    ProcessMouseDownEvent(currentEvent);
                    break;
                default:
                    break;
            }
        }

        private void ProcessMouseDownEvent(Event currentEvent)
        {
            if (currentEvent.button == 1)
            {
                ShowContextMenu(currentEvent.mousePosition);
            }
        }

        private void ShowContextMenu(Vector2 mousePosition)
        {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Create Room Node"), false, CreateRoomNode, mousePosition);
            menu.ShowAsContext();
        }

        private void CreateRoomNode(object mousePositionObject)
        {
            CreateRoomNode(mousePositionObject, _roomNodeTypeList.list.Find(x=>x.isNone));
        }
        
        private void CreateRoomNode(object mousePositionObject, RoomNodeTypeSO roomNodeType)
        {
            var mousePosition = (Vector2)mousePositionObject;
            var roomNode = CreateInstance<RoomNodeSO>();
            _currentRoomNodeGraph.roomNodeList.Add(roomNode);
            roomNode.Init(new Rect(mousePosition, new Vector2(NodeWidth, NodeHeight)), _currentRoomNodeGraph, roomNodeType);
            
            AssetDatabase.AddObjectToAsset(roomNode, _currentRoomNodeGraph);
            AssetDatabase.SaveAssets();
        }

        private void DrawRoomNodes()
        {
            _currentRoomNodeGraph.roomNodeList.ForEach(p=>p.Draw(_roomNodeStyle));
            GUI.changed = true;
        }

        private void OnEnable()
        {
            _roomNodeStyle = new GUIStyle
            {
                normal =
                {
                    background = EditorGUIUtility.Load("node1") as Texture2D,
                    textColor = Color.white
                },
                padding = new RectOffset(NodePadding, NodePadding, NodePadding, NodePadding),
                border = new RectOffset(NodeBorder, NodeBorder, NodeBorder, NodeBorder)
            };

            _roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
        }
    }
}

