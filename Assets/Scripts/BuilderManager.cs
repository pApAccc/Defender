using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class BuilderManager : MonoBehaviour
    {
        Camera mainCamera;
        BuildingTypeSO buildingType;
        List<BuildingTypeSO> buildingTypeList;
        private void Start()
        {
            mainCamera = Camera.main;
            buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name).list;
            buildingType = buildingTypeList[0];
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(buildingType.Prefab, GetMousePosition(), Quaternion.identity);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                buildingType = buildingTypeList[0];
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                buildingType = buildingTypeList[1];
            }
        }

        private Vector3 GetMousePosition()
        {
            Vector3 mouseworldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseworldPos.z = 0;
            return mouseworldPos;
        }
    }
}
