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

        /// <summary>
        /// 当活动建筑类型改变时
        /// </summary>
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
                if (activeBuildingType != null && CanSpawnBuilding(activeBuildingType, UtilsClass.GetMousePosition()))
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

        /// <summary>
        /// 判断是否能放置建筑
        /// </summary>
        /// <param name="buildingType">要创建的建筑类型</param>
        /// <param name="position">要创建的位置</param>
        /// <returns></returns>
        bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position)
        {
            BoxCollider2D boxCollider2D = buildingType.Prefab.GetComponent<BoxCollider2D>();
            Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

            bool isClear = collider2DArray.Length == 0;
            if (!isClear) return false;

            //要创建的范围是没有物体的 
            collider2DArray = Physics2D.OverlapCircleAll(UtilsClass.GetMousePosition(), buildingType.minConsrructionRadius);
            foreach (Collider2D collider2d in collider2DArray)
            {
                BuildingTypeHolder buildingTypeHolder = collider2d.GetComponent<BuildingTypeHolder>();

                //最小建造范围内是否有buildingTypeHolder
                if (buildingTypeHolder != null)
                {
                    //如果和要创建的建筑是同类的
                    if (buildingTypeHolder.buildingType == buildingType)
                    {
                        return false;
                    }
                }
            }

            //避免两个建筑的距离太过离谱
            float maxConsrructionRadius = 25;
            collider2DArray = Physics2D.OverlapCircleAll(UtilsClass.GetMousePosition(), maxConsrructionRadius);
            foreach (Collider2D collider2d in collider2DArray)
            {
                BuildingTypeHolder buildingTypeHolder = collider2d.GetComponent<BuildingTypeHolder>();
                //最小建造范围内是否有buildingTypeHolder
                if (buildingTypeHolder != null)
                {
                    //如果有建筑
                    return true;
                }
            }

            return false;
        }
    }
}
