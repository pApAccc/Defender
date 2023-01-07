using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class OptionBtn : MonoBehaviour
    {
        [SerializeField] OptionUI optionUI;
        private Button button;
        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => { ToggleActive(); });
        }
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                ToggleActive();
            }
        }
        public void ToggleActive()
        {
            optionUI.gameObject.SetActive(!optionUI.gameObject.activeSelf);

            if (optionUI.gameObject.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
