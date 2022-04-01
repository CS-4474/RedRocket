using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.DataStructures
{

    /// <summary> A timeline with the rows on the Y axis being <typeparamref name="T1"/> and the columns on the X axis (time) being <typeparamref name="T2"/>. </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    [Serializable]
    public class Timeline<T1, T2> where T1 : IEquatable<T1>
    {
        #region Constants
        private const int defaultRowLength = 150;
        #endregion

        #region Fields
        private readonly List<timelineRow> timeRows;
        #endregion

        #region Properties
        public int RowCount { get => timeRows != null ? timeRows.Count : 0; }

        public int RowLength { get; private set; }
        #endregion

        #region Constructors
        public Timeline(IReadOnlyCollection<T1> rowLabelData)
        {
            // Initialise the timeline.
            timeRows = new List<timelineRow>(rowLabelData.Count);
            foreach (T1 rowLabel in rowLabelData) AddRow(rowLabel);

            RowLength = defaultRowLength;
        }
        #endregion

        #region Get Functions
        public T1 GetRowLabel(int index) => timeRows[index].Label;

        public int GetRowIndex(T1 label)
        {
            // Go over each row until one with the matching label is found.
            for (int i = 0; i < timeRows.Count; i++) if (timeRows[i].Label.Equals(label)) return i;

            return -1;
        }

        public bool GetRowColumnData(int rowIndex, int columnIndex, out T2 data) => timeRows[rowIndex].TimeData.TryGetValue(columnIndex, out data);

        public bool GetRowColumnData(T1 label, int columnIndex, out T2 data)
        {
            // Get the row index, if it is invalid, return false.
            int rowIndex = GetRowIndex(label);
            if (rowIndex < 0) { data = default; return false; }

            return GetRowColumnData(rowIndex, columnIndex, out data);
        }
        #endregion

        #region Row Functions
        public void RefreshRows(IReadOnlyCollection<T1> rowLabelData)
        {
            for (int i = 0; i < RowCount; i++)
                timeRows[i] = new timelineRow(rowLabelData.ElementAt(i), timeRows[i].TimeData);
        }

        public void AddRow(T1 rowLabel) => timeRows.Add(new timelineRow(rowLabel));

        public void EditRowColumnData(int rowIndex, int columnIndex, T2 newValue) => timeRows[rowIndex].TimeData[columnIndex] = newValue;

        public void EditRowColumnData(T1 label, int columnIndex, T2 newValue)
        {
            // Get the row index, if it is invalid, return.
            int rowIndex = GetRowIndex(label);
            if (rowIndex < 0) return;

            EditRowColumnData(rowIndex, columnIndex, newValue);
        }
        #endregion

        #region Removal Functions
        public void RemoveRowColumnData(int rowIndex, int columnIndex) => timeRows[rowIndex].TimeData.Remove(columnIndex);

        public void RemoveRowColumnData(T1 label, int columnIndex)
        {
            // Get the row index, if it is invalid, return.
            int rowIndex = GetRowIndex(label);
            if (rowIndex < 0) return;

            RemoveRowColumnData(rowIndex, columnIndex);
        }

        public void Clear()
        {
            timeRows.Clear();
        }
        #endregion

        #region Classes
        [Serializable]
        private struct timelineRow
        {
            public T1 Label { get; set; }

            public Dictionary<int, T2> TimeData { get; private set; }

            public timelineRow(T1 label) : this(label, new Dictionary<int, T2>(defaultRowLength)) { }

            public timelineRow(T1 label, Dictionary<int, T2> timeData)
            {
                Label = label;
                TimeData = timeData;
            }
        }
        #endregion
    }
}