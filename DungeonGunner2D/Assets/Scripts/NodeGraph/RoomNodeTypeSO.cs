using UnityEngine;
using Utilities;

namespace NodeGraph
{
    [CreateAssetMenu(fileName = "RoomNodeType_", menuName = "Scriptable Objects/Dungeon/Room Node Type", order = 0)]
    public class RoomNodeTypeSO : ScriptableObject
    {
        public string roomNodeTypeName;

        [Header("Only flag the RoomNodeTypes that should be visible in editor")]
        public bool displayInNodeGraphEditor;

        [Header("One Type Should Be A Entrance")]
        public bool isEntrance;
        
        [Header("One Type Should Be A Boss Room")]
        public bool isBossRoom;
        
        [Header("One Type Should Be None (Unassigned)")]
        public bool isNone;
        
        [Header("One Type Should Be A Corridor")]
        public bool isCorridor;
        
        [Header("One Type Should Be A CorridorNS")]
        public bool isCorridorNS;
        
        [Header("One Type Should Be A CorridorEW")]
        public bool isCorridorEW;

#if UNITY_EDITOR
        private void OnValidate()
        {
            HelperUtilities.ValidateCheckEmptyString(this, nameof(roomNodeTypeName), roomNodeTypeName);
        }
#endif
    }
}