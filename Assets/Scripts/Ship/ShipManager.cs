using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts.Ship
{
    public class ShipManager : MonoBehaviour
    {
        #region Inspector Fields

        #endregion

        #region Fields
        #endregion

        #region Properties

        public IReadOnlyDictionary<string, ShipPart> PartsByName;

        public IReadOnlyList<ShipPart> Parts => PartsByName.Values.ToList();

        public IReadOnlyList<string> PartNames => PartsByName.Keys.ToList();

        public GameObject MainPart => gameObject;

        public Rigidbody2D MainBody { get; private set; }
        #endregion

        #region Initialisation Functions
        private void Awake()
        {
            MainBody = GetComponent<Rigidbody2D>();

            // Create a new list for the ship parts.
            Dictionary<string, ShipPart> shipParts = new Dictionary<string, ShipPart>();

            // Go over each child object and add any that are ship parts.
            for (int i = 0; i < transform.childCount; i++)
            {
                ShipPart shipPart = transform.GetChild(i).GetComponent<ShipPart>();

                if (shipPart != null) shipParts.Add(shipPart.name, shipPart);
            }

            PartsByName = shipParts;
        }
        #endregion

        #region Get Functions
        public ShipPart GetShipPartFromName(string name) => PartsByName[name];
        #endregion
    }
}