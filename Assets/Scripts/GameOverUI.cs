using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class GameOverUI : MonoBehaviour
    {
        public static GameOverUI Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
            transform.Find("RetryBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                GameSceneManager.Load(GameSceneManager.Scene.GameScene);
            });
            transform.Find("MainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
            });

            Hide();
        }

        public void Show()
        {
            transform.gameObject.SetActive(true);

            transform.Find("SurviveText").GetComponent<TextMeshProUGUI>().
                        SetText("你存活了 " + EnemyWaveManager.Instance.GetWaveNumber() + " 波敌人");
        }
        private void Hide()
        {
            transform.gameObject.SetActive(false);
        }
    }
}
