using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mute : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (UISetting.Instance.getAudioSetting())
        {
            audioSource.mute = false;
        }
        else
        {
            audioSource.mute = true;
        }
            
    }
}
