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
        [HideInInspector] public RoomNodeTypeSO roomNodeType;
        [HideInInspector] public RoomNodeTypeListSO roomNodeTypeList;

#if UNITY_EDITOR
        
        [HideInInspector] public Rect rect;

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
#endif
    }
}