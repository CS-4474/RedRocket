using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioChange : MonoBehaviour
{
    public Button button;
    public Sprite audioOn;
    public Sprite audioOff;

    void Start()
    {
        button = GetComponent<Button>();
        button.image.sprite = audioOn;
    }

    public void SwitchAudioImage()
    {
        button.image.sprite = button.image.sprite == audioOn ? audioOff : audioOn;
    }
}
