using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class UISetting : MonoBehaviour
{
    private bool EnableAudio;
    public event Action switchAudioSettingText = delegate { };
    public event Action SwitchAudioImage = delegate { };
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
        if (!PlayerPrefs.HasKey("Audio"))
        {
            EnableAudio = true;
            passAudioToNextScene();
        }
        else
        {
            getAudioNextScene();
        }
       
    }

    public void switchAudioMainMenu()
    {
        getAudioNextScene();
        EnableAudio = !EnableAudio;
        switchAudioSettingText();
        passAudioToNextScene();
    }

    public void switchAudioInGame()
    {
        getAudioNextScene();
        EnableAudio = !EnableAudio;
        SwitchAudioImage();
        passAudioToNextScene();
    }

    public bool getAudioSetting()
    {
        return EnableAudio;
    }

    public void passAudioToNextScene()
    {
        if (EnableAudio)
        {
            PlayerPrefs.SetInt("Audio", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Audio", 0);
        }
        PlayerPrefs.Save();
        
    }

    public void getAudioNextScene()
    {
        if (PlayerPrefs.GetInt("Audio") == 1)
        {
            EnableAudio = true;
        }
        else
        {
            EnableAudio = false;
        }
    }
}

