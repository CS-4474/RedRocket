using Assets.Scripts.DataStructures;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class ShipPart : MonoBehaviour, IEquatable<ShipPart>
    {
        #region Inspector Fields
        [SerializeField]
        private List<Command> capableCommands;
        #endregion

        #region Properties
        public IReadOnlyCollection<Command> CapableCommands => capableCommands;
        #endregion

        #region Override Functions
        public bool Equals(ShipPart other) => name == other.name;
        #endregion

        #region Command Functions
        public Command GetCapableCommandByName(string commandName)
        {
            foreach (Command command in capableCommands) if (command.Name == commandName) return command;

            return null;
        }

        public virtual void ExecuteCommand(ValueCommand command) { }
        #endregion
    }
}