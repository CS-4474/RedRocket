using Assets.Scripts.DataStructures;
using Assets.Scripts.Ship;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UITimeline : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField]
        private GameObject timelineRowPrefab;

        [SerializeField]
        private GameObject commandWindowPrefab;

        [SerializeField]
        private GameObject headerPrefab;

        [SerializeField]
        private GameObject labelPrefab;

        [SerializeField]
        private Canvas rootCanvas;

        [SerializeField]
        private Transform labelsElement;

        [SerializeField]
        private Transform gridElement;

        [SerializeField]
        private PlayerManager playerManager;
        #endregion

        #region Fields
        private TimelineData timelineData;

        private CommandSequencer commandSequencer;

        private List<UITimelineRow> rows;

        private UITimelineHeader header;

        private CommandWindowController commandWindow;
        #endregion

        #region Properties
        private Timeline<string, ValueCommand> timeline => timelineData.Data;
        #endregion

        #region Initialisation Functions
        private void Awake()
        {
            timelineData = GetComponent<TimelineData>();
            timelineData.OnDataChanged.AddListener(RecalculateContents);

            commandSequencer = GetComponent<CommandSequencer>();
            commandSequencer.OnBegin.AddListener(Begin);
            commandSequencer.OnTick += DoTick;
        }
        #endregion

        #region Calculation Functions
        public void RecalculateContents()
        {
            // Delete all child objects.
            foreach (Transform child in gridElement) Destroy(child.gameObject);
            foreach (Transform child in labelsElement) Destroy(child.gameObject);

            // Create the header.
            Text headerLabel = Instantiate(labelPrefab, labelsElement).GetComponent<Text>();
            headerLabel.text = "Ship Part";
            header = Instantiate(headerPrefab, gridElement).GetComponent<UITimelineHeader>();
            header.InitialiseTimeMarkers(timeline.RowLength);

            // Create the rows.
            rows = new List<UITimelineRow>(timeline.RowCount);
            for (int i = 0; i < timeline.RowCount; i++)
            {
                // Create the label.
                Text label = Instantiate(labelPrefab, labelsElement).GetComponent<Text>();
                label.text = timeline.GetRowLabel(i);

                // Create the new row.
                UITimelineRow newRow = Instantiate(timelineRowPrefab, gridElement).GetComponent<UITimelineRow>();

                // Add it to the list of rows.
                rows.Add(newRow);

                // Initialise it.
                newRow.Initialise(i, timeline.RowLength);
            }
        }
        #endregion

        #region Get Functions
        public Button GetTimeButton(int rowIndex, int columnIndex) => rows[rowIndex].GetButton(columnIndex);
        #endregion

        #region Timing Functions
        public void Begin()
        {
            
        }

        public void DoTick(int tick)
        {
            for (int i = 0; i < timeline.RowCount; i++)
            {
                Button button = GetTimeButton(i, tick);
                StartCoroutine("DelayChangeColour", button);
            }
        }

        private IEnumerator DelayChangeColour(Button button)
        {
            ColorBlock colours = button.colors;
            ColorBlock newcolours = colours;

            newcolours.normalColor = Color.red;

            button.colors = newcolours;

            yield return new WaitForSeconds(timelineData.TimeStep);

            if (button != null) button.colors = colours;
        }
        #endregion

        #region Window Functions
        public void ShowEditCommandWindow(int rowIndex, int tick)
        {
            // If there is already a command window, do nothing.
            if (commandWindow != null) return;

            // Create the window.
            commandWindow = Instantiate(commandWindowPrefab, rootCanvas.transform).GetComponent<CommandWindowController>();

            // Get the ship part from the row index.
            ShipPart shipPart = playerManager.Player.GetShipPartFromName(timelineData.Data.GetRowLabel(rowIndex));

            commandWindow.SetDataContext(this, timelineData, shipPart, tick);
        }
        #endregion
    }
}