using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int level;
    private int maxLevel;

    public void SelectLevel()
    {
        maxLevel = PlayerPrefs.GetInt("maxLevel", 0);
        PlayerPrefs.SetInt("SelectedLevel", level);
    }

    public void ContinueLevel()
    {
        PlayerPrefs.SetInt("SelectedLevel", maxLevel);
    }
}
