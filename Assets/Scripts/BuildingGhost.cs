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
        private void Awake()
        {
            spriteGameObject = transform.Find("Sprite").gameObject;

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
            }
            else
            {
                Show(e.activeBuildingType.sprite);
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
