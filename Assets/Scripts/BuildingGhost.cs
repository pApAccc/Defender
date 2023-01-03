using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class BuildingGhost : MonoBehaviour
    {
        GameObject spriteGameObject;
        ResourceNearbyOverlay resourceNearbyOverlay;
        private void Awake()
        {
            spriteGameObject = transform.Find("Sprite").gameObject;
            resourceNearbyOverlay = transform.Find("pfResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
            Hide();
        }

        private void Start()
        {
            BuilderManager.Instance.OnActiveBuildingTypeChanged += BuilderManager_OnActiveBuildingTypeChanged;
        }

        private void BuilderManager_OnActiveBuildingTypeChanged(object sender, BuilderManager.OnActiveBuildingTypeChangedEventArgs e)
        {
            if (e.activeBuildingType == null)
            {
                Hide();
                resourceNearbyOverlay.Hide();
            }
            else
            {
                Show(e.activeBuildingType.sprite);
                resourceNearbyOverlay.Show(e.activeBuildingType.resourceGenerateData);
            }
        }

        private void Update()
        {
            transform.position = UtilsClass.GetMousePosition();
        }

        public void Show(Sprite ghostSprite)
        {
            spriteGameObject.SetActive(true);
            spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
        }
        public void Hide()
        {
            spriteGameObject.SetActive(false);
        }
    }
}
