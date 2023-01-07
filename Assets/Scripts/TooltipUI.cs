using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class TooltipUI : MonoBehaviour
    {
        public static TooltipUI Instance { get; private set; }
        [SerializeField] RectTransform canvasRectTF;
        TextMeshProUGUI textMeshProUGUI;
        RectTransform backgroundRectTF;
        RectTransform rectTransform;
        TooltipTimer tooltipTimer;

        private void Awake()
        {
            Instance = this;
            rectTransform = GetComponent<RectTransform>();
            textMeshProUGUI = transform.Find("Text").GetComponent<TextMeshProUGUI>();
            backgroundRectTF = transform.Find("Background").GetComponent<RectTransform>();

            Hide();
        }

        public void Update()
        {
            HandleTooltipPosition();

            if (tooltipTimer != null)
            {
                tooltipTimer.timer -= Time.deltaTime;
                if (tooltipTimer.timer < 0)
                {
                    Hide();
                }
            }
        }

        private void HandleTooltipPosition()
        {
            //保证在任何分辨率下，图片在鼠标位置
            Vector2 anchoredPosition = Input.mousePosition / canvasRectTF.localScale.x;

            //保证图片不会超出右边和上面屏幕
            if (anchoredPosition.x + backgroundRectTF.rect.width > canvasRectTF.rect.width)
            {
                anchoredPosition.x = canvasRectTF.rect.width - backgroundRectTF.rect.width;
            }
            if (anchoredPosition.y + backgroundRectTF.rect.height > canvasRectTF.rect.height)
            {
                anchoredPosition.y = canvasRectTF.rect.height - backgroundRectTF.rect.height;
            }

            rectTransform.anchoredPosition = anchoredPosition;
        }

        private void SetText(string text)
        {
            textMeshProUGUI.text = text;
            textMeshProUGUI.ForceMeshUpdate();

            Vector2 textSize = textMeshProUGUI.GetRenderedValues(false);
            Vector2 padding = new Vector2(8, 8);
            backgroundRectTF.sizeDelta = textSize + padding;
        }

        public void Show(string tooltipText, TooltipTimer timer = null)
        {
            tooltipTimer = timer;
            gameObject.SetActive(true);
            SetText(tooltipText);
            HandleTooltipPosition();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public class TooltipTimer
        {
            public float timer;
        }

    }
}
