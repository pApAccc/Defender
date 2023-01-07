using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class BuildingTypeSelectUI : MonoBehaviour
    {
        Dictionary<BuildingTypeSO, Transform> btnTransformDict;
        Transform arrowBtn;
        [SerializeField] Sprite arrowSprite;
        [SerializeField] List<BuildingTypeSO> ignoreBuildingType;
        private void Awake()
        {
            //初始化
            btnTransformDict = new Dictionary<BuildingTypeSO, Transform>();
            Transform btnTemplate = transform.Find("btnTemplate");
            btnTemplate.gameObject.SetActive(false);
            BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
            int index = 0;

            //创建不属于建筑的“空”鼠标按钮
            arrowBtn = Instantiate(btnTemplate, transform);
            arrowBtn.gameObject.SetActive(true);
            arrowBtn.Find("Image").GetComponent<Image>().sprite = arrowSprite;
            arrowBtn.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(-40, -50);

            float offset = +180;
            arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * index, 0);

            arrowBtn.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingtype(null);
            });

            MouseEnterExitEvent mouseEnterExitEvent = arrowBtn.GetComponent<MouseEnterExitEvent>();
            mouseEnterExitEvent.OnMouseEnter += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Show("Arrow");
            };
            mouseEnterExitEvent.OnMouseExit += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Hide();
            };

            index++;

            //根据BuildingTypeListSO创建建筑按钮
            foreach (BuildingTypeSO buildingType in buildingTypeList.list)
            {
                if (ignoreBuildingType.Contains(buildingType)) continue;
                Transform btnTransform = Instantiate(btnTemplate, transform);
                btnTransform.gameObject.SetActive(true);

                btnTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;

                offset = +180;
                btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * index, 0);

                //注册按钮事件
                btnTransform.GetComponent<Button>().onClick.AddListener(() =>
                {
                    BuildingManager.Instance.SetActiveBuildingtype(buildingType);
                });

                //注册鼠标移入，移出事件
                mouseEnterExitEvent = btnTransform.GetComponent<MouseEnterExitEvent>();
                mouseEnterExitEvent.OnMouseEnter += (object sender, EventArgs e) =>
                {
                    TooltipUI.Instance.Show(buildingType.nameString + "\n" + buildingType.GetConstuctionResourceCostString());
                };
                mouseEnterExitEvent.OnMouseExit += (object sender, EventArgs e) =>
                {
                    TooltipUI.Instance.Hide();
                };

                btnTransformDict[buildingType] = btnTransform;
                index++;
            }
        }

        private void Start()
        {
            BuildingManager.Instance.OnActiveBuildingTypeChanged += BuilderManager_OnActiveBuildingTypeChanged;
            UpdateActiveBuildingTypeButton();
        }

        private void BuilderManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
        {
            UpdateActiveBuildingTypeButton();
        }

        /// <summary>
        /// 点击按钮后更新Selected的活动
        /// </summary>
        void UpdateActiveBuildingTypeButton()
        {
            arrowBtn.Find("Selected").gameObject.SetActive(false);
            foreach (BuildingTypeSO buildingType in btnTransformDict.Keys)
            {
                Transform btnTransform = btnTransformDict[buildingType];
                btnTransform.Find("Selected").gameObject.SetActive(false);
            }

            BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();

            if (activeBuildingType == null)
            {
                arrowBtn.Find("Selected").gameObject.SetActive(true);
            }
            else
            {
                btnTransformDict[activeBuildingType].Find("Selected").gameObject.SetActive(true);
            }

        }
    }
}
