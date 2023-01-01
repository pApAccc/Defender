using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class BuilderManager : MonoBehaviour
    {
        public static BuilderManager Instance { get; private set; }
        Camera mainCamera;
        BuildingTypeSO activeBuildingType;
        List<BuildingTypeSO> buildingTypeList;

        public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

        public class OnActiveBuildingTypeChangedEventArgs : EventArgs
        {
            public BuildingTypeSO activeBuildingType;
        }

        private void Awake()
        {
            Instance = this;
            buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name).list;
        }
        private void Start()
        {
            mainCamera = Camera.main;
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (activeBuildingType != null)
                    Instantiate(activeBuildingType.Prefab, UtilsClass.GetMousePosition(), Quaternion.identity);
            }
        }

        public void SetActiveBuildingtype(BuildingTypeSO buildingType)
        {
            activeBuildingType = buildingType;
            OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });
        }

        public BuildingTypeSO GetActiveBuildingType()
        {
            return activeBuildingType;
        }
    }
}
