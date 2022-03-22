using Assets.Scripts.DataStructures;
using Assets.Scripts.Ship;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class CommandWindowController : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField]
        private InputField valueField;

        [SerializeField]
        private Slider valueSlider;

        [SerializeField]
        private Toggle valueToggle;

        [SerializeField]
        private Dropdown commandSelector;

        [SerializeField]
        private Text sliderOutput;
        #endregion

        #region Fields
        private UITimeline uiTimeline;

        private TimelineData timelineData;

        private ShipPart shipPart;

        private int tick;
        #endregion

        #region Initialisation Functions
        private void Awake()
        {
            commandSelector.onValueChanged.AddListener(commandChanged);
            valueSlider.onValueChanged.AddListener((float newValue) => sliderOutput.text = $"{newValue:P0}");
            sliderOutput.text = $"{valueSlider.value:P0}";
        }

        public void SetDataContext(UITimeline uiTimeline, TimelineData timelineData, ShipPart shipPart, int tick)
        {
            // Set the ui, timeline, ship part, and the tick.
            this.uiTimeline = uiTimeline;
            this.timelineData = timelineData;
            this.shipPart = shipPart;
            this.tick = tick;

            // Set the options based on what the ship part is capable of.
            initialiseCommandSelector();

            // Fill in the data if there is already a command at this point.
            if (timelineData.Data.GetRowColumnData(shipPart.name, tick, out ValueCommand valueCommand)) initialiseInputField(valueCommand);
        }

        private void initialiseCommandSelector()
        {
            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>(shipPart.CapableCommands.Count);

            foreach (Command command in shipPart.CapableCommands) options.Add(new Dropdown.OptionData(command.Name));

            commandSelector.options = options;

            commandChanged(commandSelector.value);
        }

        private void commandChanged(int newSelection)
        {
            // Get the new selected command.
            Command currentCommand = shipPart.GetCapableCommandByName(commandSelector.options[newSelection].text);

            // Toggle the relevant input.
            switch (currentCommand.InputType)
            {
                case InputType.Bool:
                    valueToggle.gameObject.SetActive(true);
                    valueField.gameObject.SetActive(false);
                    valueSlider.gameObject.SetActive(false);
                    break;
                case InputType.Int:
                    valueField.contentType = InputField.ContentType.IntegerNumber;
                    valueToggle.gameObject.SetActive(false);
                    valueField.gameObject.SetActive(true);
                    valueSlider.gameObject.SetActive(false);
                    break;
                case InputType.Float:
                    valueField.contentType = InputField.ContentType.DecimalNumber;
                    valueToggle.gameObject.SetActive(false);
                    valueField.gameObject.SetActive(true);
                    valueSlider.gameObject.SetActive(false);
                    break;
                case InputType.Percentage:
                    valueToggle.gameObject.SetActive(false);
                    valueField.gameObject.SetActive(false);
                    valueSlider.gameObject.SetActive(true);
                    break;
            }
        }

        private void initialiseInputField(ValueCommand valueCommand)
        {
            switch (valueCommand.Command.InputType)
            {
                case InputType.Bool:
                    valueToggle.isOn = valueCommand.GetBoolValue();
                    break;
                case InputType.Int:
                    valueField.text = valueCommand.GetIntValue().ToString();
                    break;
                case InputType.Float:
                    valueField.text = valueCommand.GetFloatValue().ToString();
                    break;
                case InputType.Percentage:
                    valueSlider.value = valueCommand.GetFloatValue();
                    break;
            }
        }
        #endregion

        #region Window Functions
        public void Save()
        {
            string inputValue = string.Empty;
            Command command = shipPart.GetCapableCommandByName(commandSelector.options[commandSelector.value].text);

            switch (command.InputType)
            {
                case InputType.Bool:
                    inputValue = valueToggle.isOn.ToString();
                    break;
                case InputType.Int:
                case InputType.Float:
                    inputValue = valueField.text;
                    break;
                case InputType.Percentage:
                    inputValue = valueSlider.value.ToString();
                    break;
            }

            timelineData.Data.EditRowColumnData(shipPart.name, tick, new ValueCommand(inputValue, command));

            Button timeButton = uiTimeline.GetTimeButton(timelineData.Data.GetRowIndex(shipPart.name), tick);

            ColorBlock buttonColours = timeButton.colors;
            buttonColours.normalColor = new Color(0, 1, 0);
            timeButton.colors = buttonColours;
        }

        public void Close()
        {
            Destroy(gameObject);
        }

        public void Clear()
        {
            timelineData.Data.RemoveRowColumnData(shipPart.name, tick);

            Button timeButton = uiTimeline.GetTimeButton(timelineData.Data.GetRowIndex(shipPart.name), tick);

            ColorBlock buttonColours = timeButton.colors;
            buttonColours.normalColor = new Color(0.3962264f, 0.3962264f, 0.3962264f);
            timeButton.colors = buttonColours;

            Close();
        }
        #endregion
    }
}