using System.Collections;
using UnityEngine;

namespace Utilities
{
    public static class HelperUtilities
    {
        /// <summary>
        /// Empty string check
        /// </summary>
        public static bool ValidateCheckEmptyString(Object thisObject, string fieldName, string stringToCheck)
        {
            if (stringToCheck != "") 
                return false;
            
            Debug.LogError($"{fieldName} is empty and must contain a value in object {thisObject.name}");
            return true;
        }
        
        /// <summary>
        /// List empty or contains null value check - returns true if there iş an error
        /// </summary>
        public static bool ValidateCheckEnumerableValues(Object thisObject, string fieldName, IEnumerable enumerableObjectToCheck)
        {
            var error = false;
            var count = 0;

            foreach (var item in enumerableObjectToCheck)
            {
                if (item == null)
                {
                    Debug.LogError($"{fieldName} has null values in object {thisObject.name}");
                    error = true;
                    continue;
                }

                count += 1;
            }

            if (count != 0) 
                return error;
            
            Debug.LogError($"{fieldName} has no values in object {thisObject.name}");
            return true;
        }
    }
}