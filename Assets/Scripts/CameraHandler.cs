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
        [SerializeField] CinemachineVirtualCamera virtualCamera;

        float orthographicSize;
        float targetOrthographicSize;
        [SerializeField] float minOrthographicSize = 10;
        [SerializeField] float maxOrthographicSize = 20;
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
            Vector3 offset = new Vector3(x, y);

            float moveSpeed = 10f;
            transform.position += offset * moveSpeed * Time.deltaTime;

        }

    }
}
