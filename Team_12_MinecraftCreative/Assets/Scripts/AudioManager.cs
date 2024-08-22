using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public struct AudioClipInfo
    {
        public string name;
        public AudioClip clip;
    }
    public List<AudioClipInfo> audioClips;

    private Dictionary<string, AudioClip> audioDictionary;

    public AudioSource audioSource;

    void Awake()
    {
        audioDictionary = new Dictionary<string, AudioClip>();
        foreach (var audioClipInfo in audioClips)
        {
            audioDictionary[audioClipInfo.name] = audioClipInfo.clip;
        }
    }


    public void PlaySound(string soundName)
    {
        if (audioDictionary.ContainsKey(soundName))
        {
            audioSource.PlayOneShot(audioDictionary[soundName]);
        }
        else
        {
            Debug.LogWarning("Sound clp is not found: " + soundName);
        }
    }
    
}