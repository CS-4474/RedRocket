using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
public class CreditText : MonoBehaviour
{
    TextMeshProUGUI textGUI;

    private void Awake()
    {
        textGUI = GetComponent<TextMeshProUGUI>();
        String text = "Creators\n\n Original Programmer & Original Designer \n Laura Jenkins & Clara Kane\n\n CS4474 Redesign Team\n Cameron McCarthy\n Evan Rome-Bailey\n Jakob Wanger\n Jatan Patel\n Kevin Gao\n Yuchen Wang\n\n\n";
        text += "Used Assets\n\n Smoke and Fire Sprites\n Rocket Sound\n Bumping Sound\n Checkpoint Sound\n Splat Sound\n\n";
        text += "White Lotus by Kevin MacLeod (incompetech.com)\n Licensed under Creative Commons: By Attribution 3.0 License http://creativecommons.org/licenses/by/3.0/ \n\n";
        text += "Originally made for the Bournemouth University 48 hour game jam.";
        textGUI.text = text;
    }
}

