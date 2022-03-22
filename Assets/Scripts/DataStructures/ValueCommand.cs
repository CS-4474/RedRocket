using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataStructures
{
    public struct ValueCommand
    {
        #region Fields
        private string value;
        #endregion

        #region Properties
        public Command Command { get; private set; }
        #endregion

        #region Constructors
        public ValueCommand(string value, Command command)
        {
            this.value = value;
            Command = command;
        }
        #endregion

        #region Value Functions
        public string GetStringValue() => value;

        public float GetFloatValue() => float.Parse(value);

        public int GetIntValue() => int.Parse(value);

        public bool GetBoolValue() => bool.Parse(value);
        #endregion
    }
}
