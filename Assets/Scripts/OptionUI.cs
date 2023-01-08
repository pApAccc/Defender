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
    public class OptionUI : MonoBehaviour
    {
        private TextMeshProUGUI soundVolumeText;
        private TextMeshProUGUI musicVolumeText;
        private void Awake()
        {
            soundVolumeText = transform.Find("SoundVolumeText").GetComponent<TextMeshProUGUI>();
            musicVolumeText = transform.Find("MusicVolumeText").GetComponent<TextMeshProUGUI>();

            MusicAndSoundSet();
        }

        private void MusicAndSoundSet()
        {
            transform.Find("SoundIncreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                SoundManager.Instance.IncreaseVolume();
                UpdateSoundVolumeText();
            });
            transform.Find("SoundDecreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                SoundManager.Instance.DecreaseVolume();
                UpdateSoundVolumeText();
            });
            transform.Find("MusicIncreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                MusicManager.Instance.IncreaseVolume();
                UpdateMusicVolumeText();
            });
            transform.Find("MusicDecreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                MusicManager.Instance.DecreaseVolume();
                UpdateMusicVolumeText();
            });
            transform.Find("MainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
                Time.timeScale = 1.0f;
            });
        }

        private void Start()
        {
            transform.Find("EdgeScrollingToggle").GetComponent<Toggle>().onValueChanged.AddListener((bool state) =>
            {
                CameraHandler.Instance.SetScrollingState(state);
            });

            //SetIsOnWithoutNotify在设置值的时候不会调用事件
            transform.Find("EdgeScrollingToggle").GetComponent<Toggle>().SetIsOnWithoutNotify(CameraHandler.Instance.GetScrollingState());

            UpdateSoundVolumeText();
            UpdateMusicVolumeText();
            gameObject.SetActive(false);
        }

        private void UpdateSoundVolumeText()
        {
            float volume = SoundManager.Instance.GetVolume() * 10;
            soundVolumeText.SetText(Mathf.RoundToInt(volume).ToString());
        }
        private void UpdateMusicVolumeText()
        {
            float volume = MusicManager.Instance.GetVolume() * 10;
            musicVolumeText.SetText(Mathf.RoundToInt(volume).ToString());
        }


    }
}