using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public static class UtilsClass
    {
        static Camera mainCamera;
        public static Vector3 GetMousePosition()
        {
            if (mainCamera == null) mainCamera = Camera.main;

            Vector3 mouseworldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseworldPos.z = 0;
            Debug.Log(mouseworldPos);
            return mouseworldPos;
        }
    }
}
