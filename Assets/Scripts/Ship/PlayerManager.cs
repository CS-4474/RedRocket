﻿using Assets.Scripts.Spawning;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Ship
{
    public class PlayerManager : MonoBehaviour
    {
        #region Properties
        public ShipManager Player { get; private set; }

        public bool PlayerExists => !(Player is null);
        #endregion

        #region Events
        public UnityEvent OnPlayerRespawn;
        #endregion

        #region Spawn Functions
        public void SpawnPlayer(Checkpoint checkpoint)
        {
            // Destroy the old player ship.
            if (PlayerExists) Destroy(Player.gameObject);

            // Spawn a new player ship.
            Player = Instantiate(checkpoint.ShipPrefab, transform).GetComponent<ShipManager>();

            // Position the player within the checkpoint.
            checkpoint.PositionPlayer(Player.gameObject);

            // Fire the player spawn event.
            OnPlayerRespawn.Invoke();
        }
        #endregion
    }
}