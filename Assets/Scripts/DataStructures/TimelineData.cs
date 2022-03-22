using Assets.Scripts.Ship;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.DataStructures
{
    public class TimelineData : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField]
        [Range(0.01f, 1)]
        private float timeStep = 0.2f;
        #endregion

        #region Fields
        private Timeline<string, ValueCommand> data;
        #endregion

        #region Properties
        public Timeline<string, ValueCommand> Data { get => data; set { data = value; OnDataChanged.Invoke(); } }

        public float TimeStep => timeStep;
        #endregion

        #region Events
        public UnityEvent OnDataChanged;
        #endregion

        #region Reset Functions
        public void ResetData(IReadOnlyCollection<string> partNames)
        {
            Data = new Timeline<string, ValueCommand>(partNames);
        }
        #endregion
    }
}