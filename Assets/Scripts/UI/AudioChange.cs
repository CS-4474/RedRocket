using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class AudioChange : MonoBehaviour
{
    public Button button;
    public Sprite audioOn;
    public Sprite audioOff;

    private void Start()
    {
        button = GetComponent<Button>();
        UISetting.Instance.SwitchAudioImage += SwitchAudioImage;
        SwitchAudioImage();
    }

    public void SwitchAudioImage()
    {
        Debug.Log("ACCESS IMAGE");
        button.image.sprite = UISetting.Instance.getAudioSetting() == true ? audioOn : audioOff;
    }

}
