using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.DataStructures
{
    public class TimelineSaveLoader : MonoBehaviour
    {
        #region Constants
        private const string extension = ".xml";

        private const string saveFolder = "Saves";

        private const string mainNodeName = "Main";

        private const string rowNodeName = "Row";

        private const string dataNodeName = "Data";

        private const string rowAttributeName = "Length";

        private const string partNameAttributeName = "PartName";

        private const string timeAttributeName = "Tick";

        private const string valueAttributeName = "Value";

        public const string commandNameAttributeName = "CommandName";

        public const string inputNameAttributeName = "InputTypeName";

        public const string typeNameAttributeName = "CommandTypeName";
        #endregion

        #region Fields
        private TimelineData timelineData;
        #endregion

        #region Initialisation Functions
        private void Awake()
        {
            timelineData = GetComponent<TimelineData>();

            if (!Directory.Exists(Path.Combine(Application.persistentDataPath, saveFolder))) Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, saveFolder));
        }
        #endregion

        #region Load Functions
        public void Load(string fileName)
        {
            string filePath = Path.Combine(Application.persistentDataPath, saveFolder, fileName) + extension;

            // If the file doesn't exist, do nothing.
            if (!File.Exists(filePath)) return;

            XmlDocument loadDocument = new XmlDocument();
            loadDocument.Load(filePath);

            XmlNode mainNode = loadDocument.SelectSingleNode(mainNodeName);

            // Get the rows.
            XmlNodeList rowNodes = mainNode.SelectNodes(rowNodeName);

            // Load the row names into a list of strings so the timeline can be created.
            List<string> labels = new List<string>(rowNodes.Count);
            for (int i = 0; i < rowNodes.Count; i++) labels.Add(rowNodes[i].Attributes.GetNamedItem(partNameAttributeName).Value);

            // Create a new timeline object.
            Timeline<string, ValueCommand> newTimeline = new Timeline<string, ValueCommand>(labels);

            // Go over each row and add any existing data.
            for (int i = 0; i < rowNodes.Count; i++)
            {
                // Get the data points.
                XmlNodeList dataNodes = rowNodes[i].SelectNodes(dataNodeName);

                foreach (XmlNode dataNode in dataNodes)
                {
                    int tick = int.Parse(dataNode.Attributes.GetNamedItem(timeAttributeName).Value);

                    newTimeline.EditRowColumnData(i, tick, new ValueCommand(dataNode.Attributes.GetNamedItem(valueAttributeName).Value, Command.LoadFromXmlNode(dataNode)));
                }
            }

            // Set the timeline to the loaded one.
            timelineData.Data = newTimeline;
        }
        #endregion

        #region Save Functions
        public void Save(string fileName)
        {
            string filePath = Path.Combine(Application.persistentDataPath, saveFolder, fileName) + extension;

            XmlDocument saveDocument = new XmlDocument();

            // Load the file into a stream.
            FileStream fileStream = File.Exists(filePath) ? File.OpenWrite(filePath) : File.Create(filePath);

            // Reset the document.
            saveDocument.RemoveAll();

            // Create the main node and add its data.
            XmlNode mainNode = saveDocument.CreateElement(mainNodeName);
            saveDocument.AppendChild(mainNode);

            mainNode.Attributes.Append(createAttributeWithValue(saveDocument, rowAttributeName, timelineData.Data.RowLength.ToString()));

            // Create a node for each row.
            for (int i = 0; i < timelineData.Data.RowCount; i++)
            {
                XmlNode rowNode = saveDocument.CreateElement(rowNodeName);

                rowNode.Attributes.Append(createAttributeWithValue(saveDocument, partNameAttributeName, timelineData.Data.GetRowLabel(i)));

                // Go over each time value, add any that exist.
                for (int t = 0; t < timelineData.Data.RowLength; t++)
                {
                    if (timelineData.Data.GetRowColumnData(i, t, out ValueCommand valueCommand))
                    {
                        XmlNode valueNode = saveDocument.CreateElement(dataNodeName);
                        
                        valueNode.Attributes.Append(createAttributeWithValue(saveDocument, timeAttributeName, t.ToString()));
                        valueNode.Attributes.Append(createAttributeWithValue(saveDocument, valueAttributeName, valueCommand.GetStringValue()));
                        valueNode.Attributes.Append(createAttributeWithValue(saveDocument, commandNameAttributeName, valueCommand.Command.Name));
                        valueNode.Attributes.Append(createAttributeWithValue(saveDocument, inputNameAttributeName, valueCommand.Command.InputType.ToString()));
                        valueNode.Attributes.Append(createAttributeWithValue(saveDocument, typeNameAttributeName, valueCommand.Command.CommandType.ToString()));

                        rowNode.AppendChild(valueNode);
                    }
                }

                mainNode.AppendChild(rowNode);
            }

            // Save and close the file.
            saveDocument.Save(fileStream);
            fileStream.Close();
        }

        private XmlAttribute createAttributeWithValue(XmlDocument document, string name, string value)
        {
            XmlAttribute attribute = document.CreateAttribute(name);
            attribute.Value = value;

            return attribute;
        }
        #endregion

        #region Update Functions
        private void Update()
        {
            bool isCtrlDown = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

            for (KeyCode keyCode = KeyCode.F1; keyCode <= KeyCode.F12; keyCode++) if (trySaveLoad(keyCode, isCtrlDown)) break;
        }

        private bool trySaveLoad(KeyCode keyCode, bool isCtrlDown)
        {
            if (Input.GetKeyDown(keyCode))
            {
                if (isCtrlDown) Save(keyCode.ToString()); else Load(keyCode.ToString());
                return true;
            }
            else return false;
        }
        #endregion
    }
}