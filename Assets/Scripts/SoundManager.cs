using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        public enum Sound
        {
            BuildingPlaced,
            BuildingDamaged,
            BuildingDestroyed,
            EnemyDie,
            EnemyHit,
            EnemyWaveStarting,
            GameOver,
        }

        private Dictionary<Sound, AudioClip> soundAudioClipDict;
        private AudioSource audiosource;
        private float volume = .5f;
        private void Awake()
        {
            Instance = this;
            soundAudioClipDict = new Dictionary<Sound, AudioClip>();
            audiosource = GetComponent<AudioSource>();
            volume = PlayerPrefs.GetFloat("soundVolume", .5f);

            foreach (Sound sound in Enum.GetValues(typeof(Sound)))
            {
                soundAudioClipDict[sound] = Resources.Load<AudioClip>(sound.ToString());
            }
        }

        public void PlaySound(Sound sound)
        {
            audiosource.PlayOneShot(soundAudioClipDict[sound], volume);
        }
        public void IncreaseVolume()
        {
            volume += .1f;
            volume = Mathf.Clamp01(volume);
            PlayerPrefs.SetFloat("soundVolume", volume);
        }
        public void DecreaseVolume()
        {
            volume -= .1f;
            volume = Mathf.Clamp01(volume);
            PlayerPrefs.SetFloat("soundVolume", volume);
        }
        public float GetVolume()
        {
            return volume;
        }
    }
}