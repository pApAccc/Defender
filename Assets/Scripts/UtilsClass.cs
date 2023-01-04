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
            return mouseworldPos;

        }

        public static Vector3 GetRandomDir()
        {
            return new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
        }

        public static float GetAnglefromVector(Vector3 pos)
        {
            float angle = Mathf.Atan2(pos.y, pos.x);
            float degree = angle * Mathf.Rad2Deg;
            return degree;
        }
    }
}
