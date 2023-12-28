﻿using System.Collections.Generic;
using UnityEngine;

namespace NodeGraph
{
    [CreateAssetMenu(fileName = "RoomNodeGraph", menuName = "Scriptable Objects/Dungeon/Room Node Graph", order = 0)]
    public class RoomNodeGraphSO : ScriptableObject
    {
        [HideInInspector] public RoomNodeTypeListSO roomNodeTypeList;
        [HideInInspector] public List<RoomNodeSO> roomNodeList = new List<RoomNodeSO>();
        [HideInInspector] public Dictionary<string, RoomNodeSO> roomNodeDictionary = new Dictionary<string, RoomNodeSO>();

#if UNITY_EDITOR

        [HideInInspector] public RoomNodeSO roomNodeToDrawLineFrom = null;
        [HideInInspector] public Vector2 linePosition;

        public void SetNodeToDrawConnectionLineFrom(RoomNodeSO node, Vector2 position)
        {
            roomNodeToDrawLineFrom = node;
            linePosition = position;
        }
#endif
    }
}