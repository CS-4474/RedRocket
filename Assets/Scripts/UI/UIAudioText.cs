using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;

public class UIAudioText : MonoBehaviour
{
    TextMeshProUGUI textGUI;
    private void Awake()
    {
        textGUI = GetComponent<TextMeshProUGUI>();
        UISetting.Instance.switchAudioSettingText += switchAudioSettingText;
        switchAudioSettingText();
    }


    public void switchAudioSettingText()
    {
        String text = "";
        if (UISetting.Instance.getAudioSetting())
        {
            text += "Audio: ON";
        }
        else
        {
            text += "Audio: OFF";
        }
        textGUI.text = text;
    }
}
