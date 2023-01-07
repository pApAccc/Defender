using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class BuildingConstruction : MonoBehaviour
    {
        public static BuildingConstruction CreateConstruction(Vector3 position, BuildingTypeSO buildingType)
        {
            Transform pfBuildingConstruction = Resources.Load<Transform>("pfBuildingConstruction");
            Transform constructionTransform = Instantiate(pfBuildingConstruction, position, Quaternion.identity);

            BuildingConstruction buildingConstruction = constructionTransform.GetComponent<BuildingConstruction>();
            buildingConstruction.SetBuildingType(buildingType);

            return buildingConstruction;
        }

        private BuildingTypeSO buildingType;
        private float constructionTimer;
        private float constructionTimerMax;
        private BoxCollider2D boxCollider2D;
        private SpriteRenderer spriteRenderer;
        private BuildingTypeHolder buildingTypeHolder;
        private Material constructionMaterial;
        private void Awake()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            buildingTypeHolder = GetComponent<BuildingTypeHolder>();
            constructionMaterial = spriteRenderer.material;
        }
        private void Update()
        {
            constructionTimer -= Time.deltaTime;
            constructionMaterial.SetFloat("_Progress", GetConstructionTimeNormalized());

            if (constructionTimer < 0)
            {
                print("Ding");
                Instantiate(buildingType.Prefab, transform.position, Quaternion.identity);
                SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
                Destroy(gameObject);
            }
        }

        private void SetBuildingType(BuildingTypeSO buildingType)
        {
            this.buildingType = buildingType;

            constructionTimerMax = buildingType.constructionTimerMax;
            constructionTimer = constructionTimerMax;

            spriteRenderer.sprite = buildingType.sprite;
            buildingTypeHolder.buildingType = buildingType;

            boxCollider2D.offset = buildingType.Prefab.GetComponent<BoxCollider2D>().offset;
            boxCollider2D.size = buildingType.Prefab.GetComponent<BoxCollider2D>().size;
        }

        public float GetConstructionTimeNormalized()
        {
            return 1 - constructionTimer / constructionTimerMax;
        }
    }


}
