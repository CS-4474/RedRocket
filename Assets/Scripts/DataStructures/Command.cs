using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.DataStructures
{
    public enum CommandType
    {
        SetValue,
        Deploy,
        Fire,
        SetGyro
    }

    public enum InputType
    {
        None,
        Bool,
        Int,
        Float,
        Percentage
    }

    [CreateAssetMenu(fileName = "New Command", menuName = "Commands/New Command")]
    public class Command : ScriptableObject
    {
        #region Inspector Fields
        [SerializeField]
        private string name = null;

        [SerializeField]
        private CommandType commandType;

        [SerializeField]
        private InputType inputType;
        #endregion

        #region Properties
        public string Name => name;

        public CommandType CommandType => commandType;

        public InputType InputType => inputType;
        #endregion

        #region Constructors
        private Command(string name, CommandType commandType, InputType inputType)
        {
            this.name = name;
            this.commandType = commandType;
            this.inputType = inputType;
        }
        #endregion

        #region Load Functions
        public static Command LoadFromXmlNode(XmlNode node)
        {
            Command command = (Command)CreateInstance(typeof(Command));

            command.name = node.Attributes.GetNamedItem(TimelineSaveLoader.commandNameAttributeName).Value;
            command.commandType = (CommandType)Enum.Parse(typeof(CommandType), node.Attributes.GetNamedItem(TimelineSaveLoader.typeNameAttributeName).Value);
            command.inputType = (InputType)Enum.Parse(typeof(InputType), node.Attributes.GetNamedItem(TimelineSaveLoader.inputNameAttributeName).Value);

            return command;
        }
        #endregion
    }
}