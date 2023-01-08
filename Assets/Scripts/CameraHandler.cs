using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class CameraHandler : MonoBehaviour
    {
        public static CameraHandler Instance { get; private set; }
        [SerializeField] CinemachineVirtualCamera virtualCamera;

        float orthographicSize;
        float targetOrthographicSize;
        [SerializeField] float minOrthographicSize = 10;
        [SerializeField] float maxOrthographicSize = 20;

        bool isScrolling;
        private void Awake()
        {
            Instance = this;
            isScrolling = PlayerPrefs.GetInt("scrollingState", 1) == 1;
        }
        private void Start()
        {
            orthographicSize = virtualCamera.m_Lens.OrthographicSize;
            targetOrthographicSize = orthographicSize;
        }
        private void Update()
        {
            HandleMove();
            HandleZoom();
        }

        void HandleZoom()
        {
            targetOrthographicSize += -Input.mouseScrollDelta.y;
            targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

            float zoomSpeed = 5f;
            orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
            virtualCamera.m_Lens.OrthographicSize = orthographicSize;
        }

        void HandleMove()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            float edgeScrollingSize = 30;

            if (isScrolling)
            {
                if (Input.mousePosition.x > Screen.width - edgeScrollingSize)
                {
                    x = 1;
                }
                if (Input.mousePosition.x < edgeScrollingSize)
                {
                    x = -1;
                }
                if (Input.mousePosition.y > Screen.height - edgeScrollingSize)
                {
                    y = 1;
                }
                if (Input.mousePosition.y < edgeScrollingSize)
                {
                    y = -1;
                }
            }


            Vector3 offset = new Vector3(x, y);

            float moveSpeed = 18f;
            transform.position += offset * moveSpeed * Time.deltaTime;

        }

        public void SetScrollingState(bool state)
        {
            isScrolling = state;
            PlayerPrefs.SetInt("scrollingState", isScrolling ? 1 : 0);
        }
        public bool GetScrollingState()
        {
            return isScrolling;
        }
    }
}
