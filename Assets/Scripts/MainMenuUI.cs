using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class MainMenuUI : MonoBehaviour
    {
        private void Awake()
        {
            transform.Find("StartGameBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                GameSceneManager.Load(GameSceneManager.Scene.GameScene);
            });
            transform.Find("QuitGameBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }
}
