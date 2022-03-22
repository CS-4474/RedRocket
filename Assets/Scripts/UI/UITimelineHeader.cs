using Assets.Scripts.DataStructures;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UITimelineHeader : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField]
    private GameObject headerTimePrefab;
    #endregion

    #region Initialisation Functions
    public void InitialiseTimeMarkers(int columnCount)
    {
        float timeStep = GetComponentInParent<TimelineData>().TimeStep;

        for (int i = 0; i < columnCount; i++)
        {
            Text newMarker = Instantiate(headerTimePrefab, transform).GetComponent<Text>();
            newMarker.name = "Marker " + i;
            newMarker.text = $"{Math.Round(i * timeStep, 2)}s";
        }
    }
    #endregion
}
