﻿using Assets.Scripts.DataStructures;
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
        public int maxTimelineSize;

        public int maxSizeTimelineXPos;

        public int maxShipPartCount;

        [SerializeField]
        private GameObject timelineRowPrefab;

        [SerializeField]
        private GameObject commandWindowPrefab;

        [SerializeField]
        private GameObject headerPrefab;

        [SerializeField]
        private GameObject labelPrefab;

        [SerializeField]
        private GameObject labelIconPrefab;

        [SerializeField]
        private List<Sprite> labelSprites;

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

        private RectTransform timelineTransform;

        private RectTransform labelTransform;
        #endregion

        #region Properties
        private Timeline<string, ValueCommand> timeline => timelineData.Data;
        #endregion

        float[,] thrusterValues;
        Dictionary<float, Color> thrusterColorDict = new Dictionary<float, Color>(){
                { 0f, Color.white},
               { .25f, new Color(0,1,0,1)},
               { .5f, new Color(1,1,0,1)},
               { .75f,new Color(1,113f/255,0)},
               { 1f, new Color(1,0,0)},
            };

        #region Initialisation Functions
        private void Awake()
        {
            timelineData = GetComponent<TimelineData>();
            timelineData.OnDataChanged.AddListener(RecalculateContents);

            commandSequencer = GetComponent<CommandSequencer>();
            commandSequencer.OnBegin.AddListener(Begin);
            commandSequencer.OnTick += DoTick;

            timelineTransform = GetComponent<RectTransform>();
            labelTransform = labelsElement.gameObject.GetComponent<RectTransform>();
        }
        #endregion

        #region Calculation Functions
        public void RecalculateContents()
        {
            // Delete all child objects.
            foreach (Transform child in gridElement) Destroy(child.gameObject);
            foreach (Transform child in labelsElement) Destroy(child.gameObject);

            // Create the header.
            header = Instantiate(headerPrefab, gridElement).GetComponent<UITimelineHeader>();
            header.InitialiseTimeMarkers(timeline.RowLength);

            // Create the rows.
            rows = new List<UITimelineRow>(timeline.RowCount);
            for (int i = timeline.RowCount - 1; i >= 0; i--)
            {
                // Create the label.
                //Text label = Instantiate(labelPrefab, labelsElement).GetComponent<Text>();
                //label.text = timeline.GetRowLabel(i);
                Image image = Instantiate(labelIconPrefab, labelsElement).GetComponent<Image>();
                image.sprite = labelSprites[i];

                //hardcode level 1 case
                if (timeline.RowCount == 2) image.sprite = labelSprites[i + 1];

                // Create the new row.
                UITimelineRow newRow = Instantiate(timelineRowPrefab, gridElement).GetComponent<UITimelineRow>();

                // Add it to the list of rows.
                rows.Add(newRow);

                // Initialise it.
                newRow.Initialise(i, timeline.RowLength);
            }

            rows.Reverse();

            thrusterValues = new float[timeline.RowCount, timeline.RowLength];
            for (int i = 0; i < thrusterValues.GetLength(0); i++)
            {
                for (int j = 0; j < thrusterValues.GetLength(1); j++)
                {
                    thrusterValues[i, j] = 0f;
                }
            }

            Text headerLabel = Instantiate(labelPrefab, labelsElement).GetComponent<Text>();
            headerLabel.text = "Ship Part";

            timelineTransform.sizeDelta = new Vector2(timelineTransform.sizeDelta.x, maxTimelineSize * (timeline.RowCount + 3) / (maxShipPartCount + 3));
            labelTransform.sizeDelta = new Vector2(timelineTransform.sizeDelta.x, maxTimelineSize * (timeline.RowCount + 3) / (maxShipPartCount + 3));

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

        public void ToggleTurret(int row, int col)
        {
            ShipPart shipPart = playerManager.Player.GetShipPartFromName(timelineData.Data.GetRowLabel(row));
            Command command = shipPart.GetCapableCommandByName("Fire bullet");

            thrusterValues[row, col] = thrusterValues[row, col] == 1 ? 0 : 1;
            if (thrusterValues[row, col] == 0)
            {
                Color grey = new Color(101f / 255, 101f / 255, 101f / 255);

                Button timeButton = GetTimeButton(row, col);

                ColorBlock buttonColours = timeButton.colors;
                buttonColours.normalColor = grey;
                buttonColours.highlightedColor = grey;
                buttonColours.pressedColor = grey;
                timeButton.colors = buttonColours;
                timelineData.Data.RemoveRowColumnData(row, col);

            }
            else
            {

                Button timeButton = GetTimeButton(row, col);

                ColorBlock buttonColours = timeButton.colors;
                buttonColours.normalColor = Color.black;
                buttonColours.highlightedColor = Color.black;
                buttonColours.pressedColor = Color.black;
                timeButton.colors = buttonColours;
                timelineData.Data.RemoveRowColumnData(row, col);
                timelineData.Data.EditRowColumnData(row, col, new ValueCommand("", command));

            }

        }
        public void ToggleThrusterStrength(int row, int col, bool clearValue)
        {
            ShipPart shipPart = playerManager.Player.GetShipPartFromName(timelineData.Data.GetRowLabel(row));
            Command command = shipPart.GetCapableCommandByName("Set thrust to X%");

            if (clearValue)
            {
                thrusterValues[row, col] = 0f;
                timelineData.Data.RemoveRowColumnData(row, col);
                Color grey = new Color(101f / 255, 101f / 255, 101f / 255);
                Button timeButton = GetTimeButton(row, col);

                ColorBlock buttonColours = timeButton.colors;
                buttonColours.normalColor = grey;
                buttonColours.highlightedColor = grey;
                buttonColours.pressedColor = grey;
                timeButton.colors = buttonColours;
            }
            else
            {
                thrusterValues[row, col] = thrusterValues[row, col] == 0f ? 1f : thrusterValues[row, col] - .25f;

                timelineData.Data.EditRowColumnData(row, col, new ValueCommand(thrusterValues[row, col].ToString(), command));
                Button timeButton = GetTimeButton(row, col);
                ColorBlock buttonColours = timeButton.colors;
                buttonColours.normalColor = thrusterColorDict[thrusterValues[row, col]];
                buttonColours.highlightedColor = thrusterColorDict[thrusterValues[row, col]];
                buttonColours.pressedColor = thrusterColorDict[thrusterValues[row, col]];
                timeButton.colors = buttonColours;
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                for (int row = 0; row < thrusterValues.GetLength(0); row++)
                {
                    for (int col = 0; col < thrusterValues.GetLength(1); col++)
                    {
                        thrusterValues[row, col] = 0f;
                        timelineData.Data.RemoveRowColumnData(row, col);
                        Color grey = new Color(101f / 255, 101f / 255, 101f / 255);
                        Button timeButton = GetTimeButton(row, col);

                        ColorBlock buttonColours = timeButton.colors;
                        buttonColours.normalColor = grey;
                        buttonColours.highlightedColor = grey;
                        buttonColours.pressedColor = grey;
                        timeButton.colors = buttonColours;
                    }
                }

            }
        }
    }
}