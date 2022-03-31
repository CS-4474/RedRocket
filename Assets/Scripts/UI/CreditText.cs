using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
[RequireComponent(typeof(TextMeshProUGUI))]
public class CreditText : MonoBehaviour
{
    TextMeshProUGUI textGUI;

    private void Awake()
    {
        textGUI = GetComponent<TextMeshProUGUI>();
        String text = "Creators\n Original Programmer & Designer \n Cameron McCarthy\n Evan Rome-Bailey\n Jakob Wanger\n Jatan Patel\n Kevin Gao\n Yuchen Wang\n\n";
        text += "Used Assets\n Smoke and Fire Sprites\n Rocket Sound\n Bumping Sound\n Checkpoint Sound\n Splat Sound\n\n";
        text += "White Lotus Kevin MacLeod (incompetech.com) Licensed under Creative Commons: By Attribution 3.0 License http://creativecommons.org/licenses/by/3.0/ \n";
        text += "Made for the Bournemouth University 48 hour game jam.";
        textGUI.text = text;
    }
}

