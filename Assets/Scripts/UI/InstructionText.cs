using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
public class InstructionText : MonoBehaviour
{
    TextMeshProUGUI textGUI;

    private void Awake()
    {
        textGUI = GetComponent<TextMeshProUGUI>();
        String text = "Instruction\n\n";
        text += "The goal of this game is to move your rocket to the next checkpoint. To view the map, you can use mouse scrollwheel to zoom in and out.\n";
        textGUI.text = text;
    }
}

