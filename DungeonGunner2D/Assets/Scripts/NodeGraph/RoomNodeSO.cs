using System;
using System.Collections.Generic;
using GameManager;
using UnityEditor;
using UnityEngine;

namespace NodeGraph
{
    public class RoomNodeSO : ScriptableObject
    {
        [HideInInspector] public string id;
        [HideInInspector] public List<string> parentRoomNodeIDList = new List<string>();
        [HideInInspector] public List<string> childRoomNodeIDList = new List<string>();
        [HideInInspector] public RoomNodeGraphSO roomNodeGraph;
        [HideInInspector] public RoomNodeTypeListSO roomNodeTypeList;
        [HideInInspector] public RoomNodeTypeSO roomNodeType;

#if UNITY_EDITOR
        
        [HideInInspector] public Rect rect;
        [HideInInspector] public bool isDragging, isSelected;

        public void Init(Rect rect, RoomNodeGraphSO nodeGraph, RoomNodeTypeSO roomNodeType)
        {
            this.rect = rect;
            id = Guid.NewGuid().ToString();
            name = "RoomNode";
            roomNodeGraph = nodeGraph;
            this.roomNodeType = roomNodeType;

            roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
        }

        public void Draw(GUIStyle nodeStyle)
        {
            GUILayout.BeginArea(rect, nodeStyle);
            EditorGUI.BeginChangeCheck();

            var selected = roomNodeTypeList.list.FindIndex(p => p == roomNodeType);
            var selection = EditorGUILayout.Popup("", selected, GetRoomNodeTypesToDisplay());
            roomNodeType = roomNodeTypeList.list[selection];
            
            if(EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(this);
            
            GUILayout.EndArea();
        }

        public string[] GetRoomNodeTypesToDisplay()
        {
            var roomArray = new string[roomNodeTypeList.list.Count];
            for (var i = 0; i < roomNodeTypeList.list.Count; i++)
            {
                if (roomNodeTypeList.list[i].displayInNodeGraphEditor)
                    roomArray[i] = roomNodeTypeList.list[i].roomNodeTypeName;
            }

            return roomArray;
        }

        public void ProcessEvents(Event currentEvent)
        {
            switch (currentEvent.type)
            {
                case EventType.MouseDown:
                    ProcessMouseDownEvent(currentEvent);
                    break;
                case EventType.MouseDrag:
                    ProcessMouseDragEvent(currentEvent);
                    break;
                case EventType.MouseUp:
                    ProcessMouseUpEvent(currentEvent);
                    break;
                default:
                    break;
            }
        }

        private void ProcessMouseDownEvent(Event currentEvent)
        {
            if (currentEvent.button != 0)
                return; 
            
            ProcessLeftClickDownEvent();
        }

        private void ProcessMouseDragEvent(Event currentEvent)
        {
            if (currentEvent.button != 0)
                return;

            ProcessLeftClickDragEvent(currentEvent);
        }

        private void ProcessMouseUpEvent(Event currentEvent)
        {
            if (currentEvent.button != 0)
                return;

            ProcessLeftClickUpEvent();
        }
        
        private void ProcessLeftClickDownEvent()
        {
            Selection.activeObject = this;

            isSelected = !isSelected;
        }

        private void ProcessLeftClickDragEvent(Event currentEvent)
        {
            isDragging = true;
            
            DragNode(currentEvent.delta);
            GUI.changed = true;
        }

        private void ProcessLeftClickUpEvent()
        {
            if(isDragging)
                isDragging = false;
        }

        private void DragNode(Vector2 delta)
        {
            rect.position += delta;
            EditorUtility.SetDirty(this);
        }

#endif
    }
}