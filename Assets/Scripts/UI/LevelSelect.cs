using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    private int maxLevel;

    [SerializeField]
    private List<GameObject> levelList;

    void Start()
    {
        maxLevel = PlayerPrefs.GetInt("maxLevel", 0);

        for (int i = 0; i <= maxLevel; i++)
        {
            levelList[i].SetActive(true);
        }
    }
}
