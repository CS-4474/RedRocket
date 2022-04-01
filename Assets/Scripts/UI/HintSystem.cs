using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintSystem : MonoBehaviour
{
    private int maxLevel;

    //[SerializeField]
    //private Dictionary<int, GameObject> hintDictonary;

    [SerializeField]
    private List<GameObject> hintList;

    [SerializeField]
    private List<int> levels;


    void Start()
    {
        maxLevel = PlayerPrefs.GetInt("maxLevel", 0);
        
        /*
        for (int i = 0; i < hintList.Count; i++)
        {
            hintDictonary.Add(levels[i], hintList[i]);
        }
        */
    }

    public void ShowHint(int level)
    {
        /*
        GameObject hint = null;

        if (hintDictonary.TryGetValue(level, out hint))
        {
            hintDictonary[level].SetActive(true);
        }
        */

        for (int i = 0; i < hintList.Count; i++)
        {
            if(level == levels[i])
            {
                hintList[i].SetActive(true);
            }
        }
    }
}
