using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class UISetting : MonoBehaviour
{
    bool EnableAudio;
    public event Action switchAudioSettingText = delegate { };
    private static UISetting instance;
    public static UISetting Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        EnableAudio = true;
    }

    public void switchAudioSetting()
    {
        EnableAudio = !EnableAudio;
        switchAudioSettingText();
    }

    public bool getAudioSetting()
    {
        return EnableAudio;
    }
}

