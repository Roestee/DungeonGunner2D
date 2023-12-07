using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace NodeGraph
{
    [CreateAssetMenu(fileName = "RoomNodeTypeList", menuName = "Scriptable Objects/Dungeon/Room Node Type List", order = 0)]
    public class RoomNodeTypeListSO : ScriptableObject
    {
        [Space(10)][Header("Room Node Type List")]
        [Tooltip("This list should be populated with all the RoomNodeTypeSO for the game- it is used instead of an enum")]
        public List<RoomNodeTypeSO> list;

#if UNITY_EDITOR
        private void OnValidate()
        {
            HelperUtilities.ValidateCheckEnumerableValues(this, nameof(list), list);
        }
#endif
    }
}