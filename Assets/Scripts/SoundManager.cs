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
        private void Awake()
        {
            soundAudioClipDict = new Dictionary<Sound, AudioClip>();
            Instance = this;
            audiosource = GetComponent<AudioSource>();

            foreach (Sound sound in Enum.GetValues(typeof(Sound)))
            {
                soundAudioClipDict[sound] = Resources.Load<AudioClip>(sound.ToString());
            }
        }

        public void PlaySound(Sound sound)
        {
            audiosource.PlayOneShot(soundAudioClipDict[sound]);
        }
    }
}
